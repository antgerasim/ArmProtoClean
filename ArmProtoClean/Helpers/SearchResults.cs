using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArmProtoClean.Lucene;
using ArmProtoClean.Model;

namespace ArmProtoClean.Helpers
{

    internal class SearchResults
    {

        private readonly List<KeyValuePair<string, object>> _criteriaList;// ! Already member in SearchForm
        private readonly bool _isLuceneSyntax; //Default = Lucene Query Syntax
        private LuceneSearch _luceneSearch;
        private int _limit;

        public SearchResults(List<KeyValuePair<string, object>> criteriaList, int limit, bool isLuceneSyntax)
        {
            _criteriaList = criteriaList;
            _limit = limit;
            _isLuceneSyntax = isLuceneSyntax;
            //_luceneSearch = new LuceneSearch(criteriaList);
        }

        public SearchResults(List<KeyValuePair<string, object>> criteriaList, bool isLuceneSyntax)
        {
            _criteriaList = criteriaList;
            _isLuceneSyntax = isLuceneSyntax;
            //_luceneSearch = new LuceneSearch(criteriaList);
        }

        public IEnumerable<CardCriterion> SearchResultList
        {
            //if checkbox searchdefault is checked, execute search with native Lucene query syntax (SearchDefault)
            //if not, execute without
            get { return GetSearchResultBasedOnSearchType(); }
            private set { }
        }

        private IEnumerable<CardCriterion> GetSearchResultBasedOnSearchType()
        {
            _luceneSearch = new LuceneSearch(_criteriaList);
            if (_isLuceneSyntax)
                return _luceneSearch.SearchWithLuceneSyntax();
            else
                return _luceneSearch.SearchNoLuceneSyntax();
        }

        public IEnumerable<CardCriterion> GetSearchResults()
        {

            CreateSearchIndexIfNotExist();

            ShowAllIfNothingFound();

            return SearchResultList;
        }

        private void ShowAllIfNothingFound()
        {
            var minimumcriteria = _luceneSearch.SearchCriterionList.Count;
            var nothingfound = !SearchResultList.Any();

            if (minimumcriteria != 2 && nothingfound)
                SearchResultList = _luceneSearch.GetAllIndexedRecords().ToList();
        }

        //create default Lucene search index directory if not exist
        private void CreateSearchIndexIfNotExist()
        {
            if (!Directory.Exists(LuceneConfig.LuceneDir))
                Directory.CreateDirectory(LuceneConfig.LuceneDir);
        }

    }

}