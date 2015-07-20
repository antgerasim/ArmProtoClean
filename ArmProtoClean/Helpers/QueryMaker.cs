using System;
using System.Collections.Generic;
using System.Linq;
using ArmProtoClean.Lucene;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Version = Lucene.Net.Util.Version;

namespace ArmProtoClean.Helpers
{

    internal class QueryMaker
    {

        private readonly Analyzer _analyzer;
        private readonly BooleanQuery _finalQuery = new BooleanQuery();
        private readonly List<KeyValuePair<string, object>> _searchCriterion;
        private KeyValuePair<string, object> _kvpMax;
        private KeyValuePair<string, object> _kvpMin;
        private LuceneSearch _luceneSearch;

        public QueryMaker(Analyzer analyzer, LuceneSearch luceneSearch,
            List<KeyValuePair<string, object>> searchCriterion)
        {
            _analyzer = analyzer;
            _luceneSearch = luceneSearch;
            _searchCriterion = searchCriterion;

            _kvpMax = searchCriterion.First(kvp => kvp.Key.Equals("CardAdoptionDateMax"));
            _kvpMin = searchCriterion.FirstOrDefault(kvp => kvp.Key == "CardAdoptionDateMin");
        }

        public BooleanQuery MakeQuery()
        {
            //Fill BooleanQeury with anonymous terms. term.Field=critList.Key term.Term=critList.Value
            foreach (var pair in _searchCriterion)
            {
                string searchString = pair.Value.ToString();

                if (!String.IsNullOrEmpty(searchString))
                {

                    switch (pair.Key)
                    {

                        case "FindAll":

                            string[] searchFields = { "CardKind", "CardName", "CardEdition" };
                            var multiFieldQueryParser = new MultiFieldQueryParser(Version.LUCENE_30, searchFields,
                                _analyzer);

                            _finalQuery.Add(ParseQuery(searchString, multiFieldQueryParser),
                                searchString.Contains("+") ? Occur.MUST : Occur.SHOULD);
                            break;

                        case "CardAdoptionDateMin": //must be compile time constant!
                            string upperDate = DateTools.DateToString((DateTime)_kvpMax.Value, DateTools.Resolution.DAY);
                            var lowerRange = new TermRangeQuery("CardAdoptionDate", null, upperDate, true, true);
                            //{CardAdoptionDate:[* TO 20140718]}
                            _finalQuery.Add(new BooleanClause(lowerRange, Occur.MUST));
                            break;

                        case "CardAdoptionDateMax":
                            string lowerDate = DateTools.DateToString((DateTime)_kvpMin.Value, DateTools.Resolution.DAY);
                            var upperRange = new TermRangeQuery("CardAdoptionDate", lowerDate, null, true, true);
                            //{CardAdoptionDate:[20140718 TO *]}
                            _finalQuery.Add(new BooleanClause(upperRange, Occur.MUST));
                            break;

                        default:
                            _finalQuery.Add(new TermQuery(new Term(pair.Key, (string)pair.Value)), Occur.MUST);
                            break;
                    }

                    #region Source and links

                    //http://stackoverflow.com/questions/17503119/how-to-index-search-the-datetime-searchField-in-lucene-net
                    //http://stackoverflow.com/questions/3294772/storing-datetime-searchField-in-lucene-document

                    #endregion
                }
            }
            return _finalQuery;
        }

        private Query ParseQuery(string searchString, QueryParser parser)
        {
            Query query;

            try
            {
                query = parser.Parse(searchString); //Stopwords are working!

            }
            catch (ParseException)
            {

                query = parser.Parse(QueryParser.Escape(searchString));
            }
            //Singlefields:   query = {CardName:норматив*}
            //Multiplefields: query = {CardId:норматив* CardName:норматив* CardKind:норматив* CardAdoptionKindSubject:норматив* CardAdoptionSubject:норматив* Territory:норматив*}
            return query;
        }

    }

}