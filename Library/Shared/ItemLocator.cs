using System.Linq;
using Library.Cache;

using Umbraco.Web;
using Umbraco.Core.Models;
using umbraco.NodeFactory;


namespace Library.Shared
{
    public static class ItemLocator
    {
        /// <summary>
        /// Gets the site node id
        /// </summary>
        /// <returns>The site node id</returns>
        public static int GetSiteId()
        {
            if (!UmbracoContext.Current.PageId.HasValue)
                return 0;

            var currentId = Node.getCurrentNodeId();

            var currentNode = UmbracoContext.Current.ContentCache.GetById(currentId);
            var siteNode = currentNode.AncestorOrSelf(1);
            return siteNode.Id;
        }

        /// <summary>
        /// Get the Site item
        /// </summary>
        /// <returns>The site item</returns>
        public static IPublishedContent GetSiteItem()
        {
            var currentNode = UmbracoContext.Current.ContentCache.GetById(Node.GetCurrent().Id);
            var siteNode = currentNode.AncestorOrSelf(1);
            return siteNode;
        }

        /// <summary>
        ///  Gets the root configuration item
        /// </summary>
        /// <returns>The root configuration item</returns>
        public static IPublishedContent GetConfigurationItem()
        {
            return GetConfigurationItem(GetSiteId());
        }
        public static IPublishedContent GetConfigurationItem(int siteId)
        {
            var webSettings = SettingsCache.WebSettings;

            var nodeId = (from s in webSettings.Sites
                          where s.RootNodeId == siteId
                          select s.ConfigurationNodeId).First();

            var contentNode = UmbracoContext.Current.ContentCache.GetById(nodeId);
            return contentNode;
        }

        /// <summary>
        /// Gets the Site setting item
        /// </summary>
        /// <returns>The site settings item</returns>
        public static IPublishedContent GetSiteSettingsItem()
        {
            return GetSiteSettingsItem(GetSiteId());
        }
        public static IPublishedContent GetSiteSettingsItem(int siteId)
        {
            var webSettings = SettingsCache.WebSettings;

            var nodeId = (from s in webSettings.Sites
                          where s.RootNodeId == siteId
                          select s.SiteSettingsNodeId).First();

            var contentNode = UmbracoContext.Current.ContentCache.GetById(nodeId);
            return contentNode;
        }

        /// <summary>
        /// Gets the Developer setting item
        /// </summary>
        /// <returns>The developer settings item</returns>
        public static IPublishedContent GetDeveloperSettingsItem()
        {
            return GetDeveloperSettingsItem(GetSiteId());
        }
        public static IPublishedContent GetDeveloperSettingsItem(int siteId)
        {
            var webSettings = SettingsCache.WebSettings;

            var nodeId = (from s in webSettings.Sites
                          where s.RootNodeId == siteId
                          select s.DevSettingsNodeId).First();

            var contentNode = UmbracoContext.Current.ContentCache.GetById(nodeId);
            return contentNode;
        }

        /// <summary>
        /// Gets the Field Options item
        /// </summary>
        /// <returns>The field options item</returns>
        //public static IPublishedContent GetFieldOptionsItem()
        //{
        //    return GetFieldOptionsItem(GetSiteId());
        //}
        //public static IPublishedContent GetFieldOptionsItem(int siteId)
        //{
        //    var webSettings = SettingsCache.WebSettings;

        //    var nodeId = (from s in webSettings.Sites
        //                  where s.RootNodeId == siteId
        //                  select s.FieldOptionsNodeId).First();

        //    var contentNode = UmbracoContext.Current.ContentCache.GetById(nodeId);
        //    return contentNode;
        //}


       
    }
}
