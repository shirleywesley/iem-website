using System.Linq;
using System.Collections.Generic;

using Examine;

namespace Library.Shared
{
    public class ExamineQueryManager
    {
        /// <summary>
        /// Method used to search for navigation items
        /// </summary>
        /// <param name="providerName">Examine provider name</param>
        /// <param name="parentId">Parent node to start the search</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <returns>List of matching search result items</returns>
        public static IEnumerable<SearchResult> GetNavigation(string providerName, int parentId, string displayPropertyAlias, string sortField, bool sortAsc)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[providerName];
            var criteria = searcher.CreateSearchCriteria();

            var q = criteria.Field("nodeName", "*");

            if (!string.IsNullOrEmpty(displayPropertyAlias))
                q = q.And().Field(displayPropertyAlias, "1");

            if (parentId > 0)
                q = q.And().Field("parentID", parentId.ToString());

            if (!string.IsNullOrEmpty(sortField))
                q = (sortAsc) ? q.And().OrderBy(sortField) : q.And().OrderByDescending(sortField);

            if (q != null)
                criteria = q.Compile();

            var searchResult = searcher.Search(criteria).AsEnumerable();

            return searchResult;
        }

        /// <summary>
        /// Method used to search for breadcrumb items
        /// </summary>
        /// <param name="providerName">Examine provider name</param>
        /// <param name="path">The hierarchy of node items that form the breadcrumb</param>
        /// <returns>List of matching search result items</returns>
        public static IEnumerable<SearchResult> GetBreadcrumb(string providerName, string path)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[providerName];
            var criteria = searcher.CreateSearchCriteria();

            var q = criteria.Field("nodeName", "*");

            if (!string.IsNullOrEmpty(path))
                q = q.And().GroupedOr(new[] { "id" }, path.Split(','));

            q = q.And().OrderBy("id");

            if (q != null)
                criteria = q.Compile();

            var searchResult = searcher.Search(criteria).AsEnumerable();

            return searchResult;
        }

        /// <summary>
        /// Method used to search for general items
        /// </summary>
        /// <param name="searchProviderName">Examine provider name</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <param name="parentId">Parent node to start the search</param>
        /// <param name="maxItems">Maximum number of items to return</param>
        /// <returns>List of matching search result items</returns>
        public static IEnumerable<SearchResult> GetItems(string searchProviderName, string displayPropertyAlias, string sortField, bool sortAsc, int parentId, int maxItems)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[searchProviderName];
            var criteria = searcher.CreateSearchCriteria();

            var q = criteria.Field("nodeName", "*");
            q = q.Not().Field("id", parentId.ToString());

            if (!string.IsNullOrEmpty(displayPropertyAlias))
                q = q.And().Field(displayPropertyAlias, "1");

            if (!string.IsNullOrEmpty(sortField))
                q = (sortAsc) ? q.And().OrderBy(sortField) : q.And().OrderByDescending(sortField);

            if (q != null)
                criteria = q.Compile();

            var searchResult = searcher.Search(criteria).AsEnumerable();

            if (parentId > 0)
                searchResult = searchResult.Where(x => x.Fields["path"].Split(',').Any(y => y == parentId.ToString()));

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortField.Equals("sortOrder"))
                {
                    searchResult = sortAsc
                        ? searchResult.OrderBy(x => int.Parse(x.Fields[sortField]))
                        : searchResult.OrderByDescending(x => int.Parse(x.Fields[sortField]));
                }
                else
                {
                    searchResult = sortAsc
                        ? searchResult.OrderBy(x => x.Fields[sortField])
                        : searchResult.OrderByDescending(x => x.Fields[sortField]);
                }
            }

            return (maxItems > 0) ?
                searchResult.Take(maxItems)
                    : searchResult;
        }

        /// <summary>
        /// Method used to search for a paged subset of general items
        /// </summary>
        /// <param name="searchProviderName">Examine provider name</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <param name="parentId">Parent node to start the search</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageNumber">Current page number</param>
        /// <returns>List of matching search result items</returns>
        public static PagedResult GetItems(string searchProviderName, string displayPropertyAlias, string sortField, bool sortAsc, int parentId, int pageSize, int pageNumber)
        {
            var searchResults = GetItems(searchProviderName, displayPropertyAlias, sortField, sortAsc, parentId, 0);

            return new PagedResult
            {
                Results = searchResults.Skip(pageSize * (pageNumber - 1)).Take(pageSize),
                TotalCount = searchResults.Count()
            };
        }

        /// <summary>
        /// Method used to search for general items
        /// </summary>
        /// <param name="providerName">Examine provider name</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <param name="parentId">The direct parent node to start the search</param>
        /// <param name="maxItems">Maximum number of items to return</param>
        /// <returns>List of matching search result items</returns>
        public static IEnumerable<SearchResult> GetItemsByParentId(string providerName, string displayPropertyAlias, string sortField, bool sortAsc, int parentId, int maxItems)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[providerName];
            var criteria = searcher.CreateSearchCriteria();

            var q = criteria.Field("parentID", parentId.ToString());

            if (!string.IsNullOrEmpty(displayPropertyAlias))
                q = q.And().Field(displayPropertyAlias, "1");

            if (!string.IsNullOrEmpty(sortField))
                q = (sortAsc) ? q.And().OrderBy(sortField) : q.And().OrderByDescending(sortField);

            if (q != null)
                criteria = q.Compile();

            var searchResult = searcher.Search(criteria).AsEnumerable();

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortField.Equals("sortOrder"))
                {
                    searchResult = sortAsc
                        ? searchResult.OrderBy(x => int.Parse(x.Fields[sortField]))
                        : searchResult.OrderByDescending(x => int.Parse(x.Fields[sortField]));
                }
                else
                {
                    searchResult = sortAsc
                        ? searchResult.OrderBy(x => x.Fields[sortField])
                        : searchResult.OrderByDescending(x => x.Fields[sortField]);
                }
            }

            return (maxItems > 0) ?
                searchResult.Take(maxItems)
                    : searchResult;
        }

        /// <summary>
        /// Method used to search for a paged subset of general items
        /// </summary>
        /// <param name="providerName">Examine provider name</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <param name="parentId">The direct parent node to start the search</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageNumber">Current page number</param>
        /// <returns>List of matching search result items</returns>
        public static PagedResult GetItemsByParentId(string providerName, string displayPropertyAlias, string sortField, bool sortAsc, int parentId, int pageSize, int pageNumber)
        {
            var searchResults = GetItemsByParentId(providerName, displayPropertyAlias, sortField, sortAsc, parentId, 0);

            return new PagedResult
            {
                Results = (pageSize > 0) ? searchResults.Skip(pageSize * (pageNumber - 1)).Take(pageSize) : searchResults,
                TotalCount = searchResults.Count()
            };
        }

        /// <summary>
        /// Method used to search for general items by document type
        /// </summary>
        /// <param name="searchProviderName">Examine provider name</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <param name="nodeTypeList">The list of document type aliases</param>
        /// <param name="parentIdList">The list of parent nodes to search from</param>
        /// <param name="maxItems">Maximum number of items to return</param>
        /// <returns>List of matching search result items</returns>
        public static IEnumerable<SearchResult> GetItemsByType(string searchProviderName, string displayPropertyAlias, string sortField, bool sortAsc, IEnumerable<string> nodeTypeList, IEnumerable<string> parentIdList, int maxItems)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[searchProviderName];
            var criteria = searcher.CreateSearchCriteria();

            var q = criteria.Field("nodeName", "*");

            if (nodeTypeList.Any())
                q = q.And().GroupedOr(new[] { "nodeTypeAlias" }, nodeTypeList.ToArray());

            if (!string.IsNullOrEmpty(displayPropertyAlias))
                q = q.And().Field(displayPropertyAlias, "1");

            if (!string.IsNullOrEmpty(sortField))
                q = (sortAsc) ? q.And().OrderBy(sortField) : q.And().OrderByDescending(sortField);

            if (q != null)
                criteria = q.Compile();

            var searchResult = searcher.Search(criteria).AsEnumerable();

            if (parentIdList.Any())
                searchResult = searchResult.Where(x => x.Fields["path"].Split(',').Any(y => parentIdList.Any(z => z == y)));

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortField.Equals("sortOrder"))
                {
                    searchResult = sortAsc
                        ? searchResult.OrderBy(x => int.Parse(x.Fields[sortField]))
                        : searchResult.OrderByDescending(x => int.Parse(x.Fields[sortField]));
                }
                else
                {
                    searchResult = sortAsc
                        ? searchResult.OrderBy(x => x.Fields[sortField])
                        : searchResult.OrderByDescending(x => x.Fields[sortField]);
                }
            }

            return (maxItems > 0) ?
                searchResult.Take(maxItems)
                    : searchResult;
        }

        /// <summary>
        /// Method used to search for a paged subset of general items by document type
        /// </summary>
        /// <param name="searchProviderName">Examine provider name</param>
        /// <param name="displayPropertyAlias">The property to determine the visibility of the node</param>
        /// <param name="sortField">The sort field</param>
        /// <param name="sortAsc">Sort ascending toggle value</param>
        /// <param name="nodeTypeList">The list of document type aliases</param>
        /// <param name="parentIdList">The list of parent nodes to search from</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageNumber">Current page number</param>
        /// <returns>List of matching search result items</returns>
        public static PagedResult GetItemsByType(string searchProviderName, string displayPropertyAlias, string sortField, bool sortAsc, IEnumerable<string> nodeTypeList, IEnumerable<string> parentIdList, int pageSize, int pageNumber)
        {
            var searchResults = GetItemsByType(searchProviderName, displayPropertyAlias, sortField, sortAsc, nodeTypeList, parentIdList, 0);

            return new PagedResult
            {
                Results = searchResults.Skip(pageSize * (pageNumber - 1)).Take(pageSize),
                TotalCount = searchResults.Count()
            };
        }
    }

    public class PagedResult
    {
        public IEnumerable<SearchResult> Results { get; set; }
        public int TotalCount { get; set; }
    }
}


