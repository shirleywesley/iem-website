using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Library.Extensions;
using Library.Shared;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Library.Cache
{
    [DataContract]
    public class DeveloperSettings
    {
        /// <summary>
        /// This class accesses developer setting data from the cache
        /// </summary>

        public static int Id
        {
            get
            {
                var settingsItem = ItemLocator.GetDeveloperSettingsItem();
                return settingsItem.Id;
            }
        }

        /// <summary>
        /// Gets the list of parent nodes of news items
        /// </summary>
        /// <returns>The selected parent content items</returns>
        public static IEnumerable<IPublishedContent> NewsParent
        {
            get
            {
                var settingsItem = ItemLocator.GetDeveloperSettingsItem();

                return settingsItem.GetMultiPickerItems(UmbracoAliases.NewsRootNode);
            }
        }

        ///// <summary>
        ///// Gets the list of document type ids that are excluded from the breadcrumb
        ///// </summary>
        ///// <returns>The selected document type ids</returns>
        //public static IEnumerable<string> BreadcrumbExclusionTypes
        //{
        //    get
        //    {
        //        var settingsItem = ItemLocator.GetDeveloperSettingsItem();

        //        var siteId = ItemLocator.GetSiteId();
        //        var webSettingItem = SettingsCache.WebSettings.Aliases;
        //        var propertyAlias = "breadcrumbExclusionTypes";

        //        if (webSettingItem != null)
        //            propertyAlias = webSettingItem.AliasBreadcrumbExclusionTypes;

        //        var items = settingsItem.GetPropertyValue<IEnumerable<string>>(propertyAlias);

        //        return items;
        //    }
        //}

        ///// <summary>
        ///// Gets the list of document type ids that are hidden in the breadcrumb
        ///// </summary>
        ///// <returns>The selected document type ids</returns>
        //public static IEnumerable<string> BreadcrumbHiddenTypes
        //{
        //    get
        //    {
        //        var settingsItem = ItemLocator.GetDeveloperSettingsItem();

        //        var siteId = ItemLocator.GetSiteId();
        //        var webSettingItem = SettingsCache.WebSettings.Aliases;
        //        var propertyAlias = "breadcrumbHiddenTypes";

        //        if (webSettingItem != null)
        //            propertyAlias = webSettingItem.AliasBreadcrumbHiddenTypes;

        //        var items = settingsItem.GetPropertyValue<IEnumerable<string>>(propertyAlias);

        //        return items;
        //    }
        //}

        /// <summary>
        /// Gets the home node id
        /// </summary>
        /// <returns>The home node id as a string value</returns>
        public static string HomePage
        {
            get
            {
                var settingsItem = ItemLocator.GetDeveloperSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.HomeNode);
            }
        }

        /// <summary>
        /// Header Scripts
        /// </summary>
        public static string HeaderScripts
        {
            get
            {
                var settingsItem = ItemLocator.GetDeveloperSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.HeaderScripts);
            }
        }

        /// <summary>
        /// Footer Scripts
        /// </summary>
        public static string FooterScripts
        {
            get
            {
                var settingsItem = ItemLocator.GetDeveloperSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.FooterScripts);
            }
        }
    }
}
