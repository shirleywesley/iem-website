using System.Linq;
using System.Runtime.Serialization;
using Library.Extensions;
using Library.Shared;
using RJP.MultiUrlPicker.Models;

namespace Library.Cache
{
    [DataContract]
    public class SiteSettings
    {
        public static int Id
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
                return settingsItem.Id;
            }
        }

        /// <summary>
        /// Gets the site title
        /// </summary>
        /// <returns>string representation of the site title</returns> 
        public static string SiteTitle
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.SiteTitle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SiteLogo
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
                
                return settingsItem.GetMediaItemUrl(UmbracoAliases.SiteLogo);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static string MetaDescriptionDefault
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.MetaDescriptionDefault);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static string MetaKeywordsDefault
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
           
                return settingsItem.GetTextFieldValue(UmbracoAliases.MetaKeywordsDefault);
            }
        }


        /// <summary>
        /// Gets the footer copyright
        /// </summary>
        /// <returns>string representation of the footer copyright</returns>
        public static string FooterCopyright
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.FooterCopyright);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static string DefaultShareImage
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
             
                return settingsItem.GetMediaItemUrl(UmbracoAliases.DefaultShareImage);
            }
        }


        /// <summary>
        /// Gets the specified links for the footer area
        /// </summary>
        /// <returns>list of UrlPickerContent items</returns>
        public static MultiUrls FooterNavigation
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetMultiPickerLinks(UmbracoAliases.FooterQuicklinks);
            }
        }

        /// <summary>
        /// Gets the Google Analytics Id
        /// </summary>
        /// <returns>string representation of the Google analytics id</returns>
        public static string GoogleAnalyticsID
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.GoogleAnalyticsID);
            }
        }

        /// <summary>
        /// Gets the Google Analytics Domain
        /// </summary>
        /// <returns>string representation of the Google analytics domain</returns>
        public static string GoogleAnalyticsDomain
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
            
                return settingsItem.GetTextFieldValue(UmbracoAliases.GoogleAnalyticsDomain);
            }
        }

        public static string SocialSharingChannels
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.SocialSharingChannels);
            }
        }

        /// <summary>
        /// Gets the Twitter username
        /// </summary>
        /// <returns>string representation of the Twitter username</returns>
        public static string TwitterUsername
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.TwitterUsername);
            }
        }

        /// <summary>
        /// Gets the Facebook username
        /// </summary>
        /// <returns>string representation of the Facebook username</returns>
        public static string FacebookUsername
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
           
                return settingsItem.GetTextFieldValue(UmbracoAliases.FacebookUsername);
            }
        }

        /// <summary>
        /// Gets the G+ username
        /// </summary>
        /// <returns>string representation of the G+ username</returns>
        public static string GooglePlusUsername
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
            
                return settingsItem.GetTextFieldValue(UmbracoAliases.GooglePlusUsername);
            }
        }


        /// <summary>
        /// Gets the LinkedIn username
        /// </summary>
        /// <returns>string representation of the LinkedIn username</returns>
        public static string LinkedInUsername
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();

                return settingsItem.GetTextFieldValue(UmbracoAliases.LinkedInUsername);
            }
        }

        /// <summary>
        /// Gets the YouTube username
        /// </summary>
        /// <returns>string representation of the YouTube username</returns>
        public static string YouTubeUsername
        {
            get
            {
                var settingsItem = ItemLocator.GetSiteSettingsItem();
          
                return settingsItem.GetTextFieldValue(UmbracoAliases.YouTubeUsername);
            }
        }

    }
}
