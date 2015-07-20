using System;
using System.Collections.Generic;
using ArmProtoClean.Model;
using Lucene.Net.Analysis.Ru;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Version = Lucene.Net.Util.Version;

namespace ArmProtoClean.Lucene
{

    public static class LuceneIndex
    {

        // add/update/clear search index data 
        public static void AddUpdateLuceneIndex(CardCriterion cardCriterion)
        {
            AddUpdateLuceneIndex(new List<CardCriterion> {cardCriterion});
        }

        public static void AddUpdateLuceneIndex(IEnumerable<CardCriterion> cardCriteria)
        {
            // init lucene
            using (var analyzer = new RussianAnalyzer(Version.LUCENE_30))
                //using (var analyzer = new SnowballAnalyzer(Version.LUCENE_30, "Russian"))//Includes stopwords? if not, create a GetStopWordslist() method
            using (var writer = new IndexWriter(LuceneConfig.Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entries if any)
                foreach (var criterion in cardCriteria)
                    _addToLuceneIndex(criterion, writer);
            }
        }

        public static void ClearLuceneIndexRecord(int record_id)
        {
            // init lucene
            using (var analyzer = new RussianAnalyzer(Version.LUCENE_30))
            using (var writer = new IndexWriter(LuceneConfig.Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("Id", record_id.ToString()));
                writer.DeleteDocuments(searchQuery);
            }
        }

        public static bool ClearLuceneIndex()
        {
            try
            {
                using (var analyzer = new RussianAnalyzer(Version.LUCENE_30))
                using (
                    var writer = new IndexWriter(LuceneConfig.Directory, analyzer, true,
                        IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void Optimize()
        {
            using (var analyzer = new RussianAnalyzer(Version.LUCENE_30))
            using (var writer = new IndexWriter(LuceneConfig.Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                writer.Optimize();
        }

        private static void _addToLuceneIndex(CardCriterion cardCriterion, IndexWriter writer)
        {
            //Add Tikafields from CardCriteriaRepository maybe here later to avoid RAM-haevy traffic?
            // remove older index entry
            TermQuery searchQuery = new TermQuery(new Term("EditionId", Convert.ToString(cardCriterion.EditionId)));
            writer.DeleteDocuments(searchQuery);
            // add new index entry
            Document doc = new Document();
            // add lucene fields mapped to db fields
            doc.Add(new Field("EditionId", Convert.ToString(cardCriterion.EditionId), Field.Store.YES,
                Field.Index.NOT_ANALYZED));
            doc.Add(new Field("CardId", Convert.ToString(cardCriterion.CardId), Field.Store.YES,
                Field.Index.NOT_ANALYZED));
            //doc.Add(new NumericField("CardId").SetIntValue(cardCriterion.CardId));
            doc.Add(new Field("CardName", cardCriterion.CardName, Field.Store.YES, Field.Index.ANALYZED));
            //doc.Add(new Field("CardName", cardCriterion.CardName, Field.Store.NO,
            //    Field.Index.ANALYZED));
            doc.Add(new Field("CardKind", cardCriterion.CardKind, Field.Store.YES, Field.Index.NOT_ANALYZED));
            //Changed from analyzed
            doc.Add(new Field("FileExt", cardCriterion.FileExt, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("CardEdition", cardCriterion.CardEdition, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("CardAdoptionKindSubject", cardCriterion.CardAdoptionKindSubject, Field.Store.YES,
                Field.Index.NOT_ANALYZED));
            //Changed from analyzed
            doc.Add(new Field("CardAdoptionSubject", cardCriterion.CardAdoptionSubject, Field.Store.YES,
                Field.Index.NOT_ANALYZED));
            //Changed from analyzed
            doc.Add(new Field("Territory", cardCriterion.Territory, Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new Field("CardEdition", cardCriterion.CardEdition, Field.Store.NO,
            //    Field.Index.ANALYZED));
            doc.Add(new Field("CardAdoptionNumber", cardCriterion.CardAdoptionNumber, Field.Store.YES,
                Field.Index.NOT_ANALYZED));
            doc.Add(new Field("CardAdoptionDate",
                DateTools.DateToString(cardCriterion.CardAdoptionDate, DateTools.Resolution.DAY), Field.Store.YES,
                Field.Index.NOT_ANALYZED));

            #region Index Metadata

            //doc.Add(new Field("ContentType", cardCriterion.ContentType, Field.Store.YES,
            //    Field.Index.ANALYZED)); No need to index contentype so far
            //foreach (var kvp in cardCriterion.ContentMetadataDict)
            //{
            //    doc.Add(new Field(kvp.Key, kvp.Value, Field.Store.YES, Field.Index.NOT_ANALYZED));
            //} //Add later if metadata indexing is needed

            #endregion

            // add entry to index
            writer.AddDocument(doc);
        }

    }

}