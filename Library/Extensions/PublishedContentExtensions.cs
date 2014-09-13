// uComponents currently not supported in Umbraco v.7
//using uComponents.DataTypes.DataTypeGrid.Model;
//using uComponents.DataTypes.UrlPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Shared;
using RJP.MultiUrlPicker.Models;
using umbraco;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;
using Umbraco.Web;

// uComponents currently not supported in Umbraco v.7
//using uComponents.DataTypes.UrlPicker.Dto;

namespace Library.Extensions
{
    public static class PublishedContentExtensions
    {
        /// <summary>
        /// Deserializes a URLPicker. This method converts the XML data representation of a URLPicker into the UrlPickerContent 
        /// object for easy manipulation in the code.
        /// </summary>
        /// <param name="item">The item containing the URLPicker property</param>
        /// <param name="nodePropertyAlias">The alias of the property containing the URL Picker data</param>
        /// <returns>UrlPickerContent - The Object representation of the URLPicker data</returns>
        
        
        public static UrlPickerContent DeserializeUrlPicker(this IPublishedContent item, string nodePropertyAlias)
        {
            var url = item.Properties.Any(x => x.PropertyTypeAlias.Equals(nodePropertyAlias, StringComparison.InvariantCultureIgnoreCase)) ? item.GetPropertyValue<string>(nodePropertyAlias) : "";
            return new UrlPickerContent() { Url = url };
        }

        /// <summary>
        /// Gets the string value in a textbox
        /// </summary>
        /// <param name="item">The item containing the property</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The string value in the textbox</returns>
        public static string GetTextFieldValue(this IPublishedContent item, string propertyName)
        {
            return item.HasProperty(propertyName) ? item.GetPropertyValue<string>(propertyName) : null;
        }


        /// <summary>
        /// get a link to a media item 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetMediaItemUrl(this IPublishedContent item, string propertyName)
        {
            if (!item.HasProperty(propertyName))
                return string.Empty;

            UmbracoHelper helper = new UmbracoHelper();
            var property = item.GetPropertyValue<string>(propertyName);
            var media = umbraco.uQuery.GetMedia(property);

            return (media != null) ? media.GetImageUrl() : string.Empty;
        }

      
        /// <summary>
        /// Gets the pageTitle field or the nodeName if the prior is empty
        /// </summary>
        /// <param name="item">The item containing the multipicker field</param>
        /// <returns>String value in either field (pagetitle, nodename)</returns>
        public static string TitleOrName(this IPublishedContent item)
        {
            return item.GetPropertyValue("pageTitle") != null && !String.IsNullOrWhiteSpace(item.GetPropertyValue<string>("pageTitle")) ? item.GetPropertyValue<string>("pageTitle") : item.Name;
        }

        /// <summary>
        /// Gets the multipicker data from the supplied field
        /// </summary>
        /// <param name="item">The item containing the multipicker field</param>
        /// <param name="propertyName">The field alias containing the multipicker data</param>
        /// <returns>IEnumerable list of UrlPickerSates</returns>

        // uComponents currently not supported in Umbraco v.7
        public static MultiUrls GetMultiPickerLinks(this IPublishedContent item, string propertyName)
        {
            return item.GetPropertyValue<MultiUrls>(propertyName);
        }

        /// <summary>
        /// Gets the list of multipicker item data
        /// </summary>
        /// <param name="item">The item containing the multipicker field</param>
        /// <param name="propertyName">The name of the field containing the multipicker data</param>
        /// <returns>A list of IContent objects</returns>
        public static IEnumerable<IPublishedContent> GetMultiPickerItems(this IPublishedContent item, string propertyName)
        {
            try
            {
                // try to process the multi-picker as XML
                var multiPickerProperty = new DynamicXml(item.GetPropertyValue<string>(propertyName));
                var returnList = new List<IPublishedContent>();

                foreach (var nextItem in multiPickerProperty)
                {
                    try
                    {
                        var node = UmbracoContext.Current.ContentCache.GetById(Convert.ToInt32(nextItem.InnerText));
                        if (node != null)
                        {
                            returnList.Add(node);
                        }
                    }
                    catch
                    {
                        // obtaining data for one of the items 
                        // fails then fail quitely and continue processing 
                    }
                }
                return returnList;
            }
            catch (Exception)
            {
                // if processing the picker as XML faiiled it must be CSV format so process as CSV
                var ids = item.GetPropertyValue<string>(propertyName).Split(',');
                var returnList = new List<IPublishedContent>();
                foreach (var nextItem in ids)
                {
                    try
                    {
                        var node = UmbracoContext.Current.ContentCache.GetById(Convert.ToInt32(nextItem));
                        if (node != null)
                        {
                            returnList.Add(node);
                        }
                    }
                    catch
                    {
                        // obtaining data for one of the items 
                        // fails then fail quitely and continue processing 
                    }
                }
                return returnList;
            }
        }

    }
}


