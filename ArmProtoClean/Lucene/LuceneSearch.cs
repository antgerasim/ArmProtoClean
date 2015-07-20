using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArmProtoClean.Helpers;
using ArmProtoClean.Model;
using ikvm.extensions;
using Lucene.Net.Analysis.Ru;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Version = Lucene.Net.Util.Version;

namespace ArmProtoClean.Lucene
{

    public class LuceneSearch
    {

        private readonly DateTime _maxDate;
        private readonly DateTime _minDate;

        public LuceneSearch(List<KeyValuePair<string, object>> searchCriterionList)
        {
            SearchCriterionList = searchCriterionList;
            _minDate = (DateTime) searchCriterionList.First(kvp => kvp.Key.Equals("CardAdoptionDateMin")).Value;
            _maxDate = (DateTime) searchCriterionList.First(kvp => kvp.Key.Equals("CardAdoptionDateMax")).Value;

        }

        public LuceneSearch() { }

        public List<KeyValuePair<string, object>> SearchCriterionList { get; private set; }

        // search methods
        public IEnumerable<CardCriterion> GetAllIndexedRecords()
        {
            // validate search index
            if (!Directory.EnumerateFiles(LuceneConfig.LuceneDir).Any())
                return new List<CardCriterion>();

            // set up lucene searcher
            List<Document> docList;
            using (var searcher = new IndexSearcher(LuceneConfig.Directory, false))
            using (var reader = IndexReader.Open(LuceneConfig.Directory, false))
            {
                docList = new List<Document>();
                var term = reader.TermDocs();

                while (term.Next())
                    docList.Add(searcher.Doc(term.Doc));
            }
            return _mapLuceneToDataList(docList);
        }

        public IEnumerable<CardCriterion> SearchWithLuceneSyntax()
        {
            if (SearchCriterionList.Count != 2 && _minDate == DateTime.MinValue && _maxDate == DateTime.Today)
                return new List<CardCriterion>();
            return _search(SearchCriterionList);
        }

        public IEnumerable<CardCriterion> SearchNoLuceneSyntax()
        {
            if (SearchCriterionList.Count != 2 && _minDate == DateTime.MinValue && _maxDate == DateTime.Today)
                return new List<CardCriterion>();
            FilterDict(SearchCriterionList);
            return _search(SearchCriterionList);
        }

        //Method-object candidate
        private void FilterDict(List<KeyValuePair<string, object>> searchCriterion) //not trimmed up to here
        {
            foreach (KeyValuePair<string, object> oldPair in
                searchCriterion.Where(kvp => !string.IsNullOrEmpty(kvp.Value.toString())).ToList())
            {
                //if (oldPair.Key.Equals("CardName") || oldPair.Key.Equals("CardEdition"))
                if (oldPair.Key.Equals("FindAll"))
                {
                    //Main Filter Section. Filter special sign here (no * addition).
                    var newPair = new KeyValuePair<string, object>(oldPair.Key, GetFilteredString(oldPair));

                    //Update cardCriteria
                    if (searchCriterion.Contains(oldPair))
                        searchCriterion.Remove(oldPair);

                    searchCriterion.Add(newPair);
                }
            }
        }

        //Invoke only for user entered values. Not for pre-entered values in Comboboxes.
        private string GetFilteredString(KeyValuePair<string, object> kvpSearchCriterion)
        {
            #region old

            //IEnumerable<string> filteredStrings = kvpSearchCriterion.Value.toString()
            //    .trim()
            //    .Replace("-", " ") //remove any hyphen before tokenization
            //    .replace("_", " ") //remove any underline before tokenization
            //    .Replace("\"", "") //remove any double quotation (") marks
            //    .Replace("\'", "") //remove any single quotation (') marks
            //    .Replace("«", "") //remove russian opening quotation («) marks
            //    .Replace("»", "") //remove russian closing quotation (») marks
            //    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            ////split and add asterisk * for every token
            ////.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.trim() + "*").ToArray();
            //string filteredString = string.Join(" ", filteredStrings);
            //return string.Join(" ", filteredStrings);

            #endregion

            string filteredString = kvpSearchCriterion.Value.toString()
                .trim()
                .Replace("-", " ") //remove any hyphen before tokenization
                .Replace("_", " ") //remove any underline before tokenization
                .Replace("\"", "") //remove any double quotation (") marks
                .Replace("\'", "") //remove any single quotation (') marks
                .Replace("«", "") //remove russian opening quotation («) marks
                .Replace("»", ""); //remove russian closing quotation (») marks

            return filteredString;
        }

        // main search method
        private IEnumerable<CardCriterion> _search(List<KeyValuePair<string, object>> searchCriterion)
        {

            //ifdict.Value contains * OR ?, replace. If After this, searchString is empty, return new List<>
            if (searchCriterion.Where(kvp => string.IsNullOrEmpty(kvp.Value.toString().Replace("?", ""))).ToList().Any())
                return new List<CardCriterion>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(LuceneConfig.Directory, false))
            {

                IEnumerable<CardCriterion> results; //rename to resultlist

                using (var analyzer = new RussianAnalyzer(Version.LUCENE_30)) //contains already a russian stopwordlist
                {

                    Query query = new QueryMaker(analyzer, this, searchCriterion).MakeQuery();
                    ScoreDoc[] hits = searcher.Search(query, LuceneConfig.HitsLimit).ScoreDocs;
                    results = _mapLuceneToDataList(hits, searcher);
                }
                return results;
            }
        }

        // map Lucene search index to data
        private IEnumerable<CardCriterion> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }

        private IEnumerable<CardCriterion> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits, IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        private CardCriterion _mapLuceneDocumentToData(Document doc) //mapHitToDataModel
        {
            return new CardCriterion
                //by creating a new Modelobject from scratch, fields dateMin and Max are setted up to default
            {
                EditionId = Convert.ToUInt32(doc.Get("EditionId")),
                CardId = Convert.ToUInt32(doc.Get("CardId")),
                Territory = doc.Get("Territory"),
                CardKind = doc.Get("CardKind"),
                CardName = doc.Get("CardName").Trim(),
                CardEdition = doc.Get("CardEdition").Trim(),
                FileExt = doc.Get("FileExt"),
                CardAdoptionKindSubject = doc.Get("CardAdoptionKindSubject"),
                CardAdoptionSubject = doc.Get("CardAdoptionSubject"),
                CardAdoptionDate = DateTools.StringToDate(doc.Get("CardAdoptionDate")),
                CardAdoptionNumber = doc.Get("CardAdoptionNumber"),
            };
        }

    }

}