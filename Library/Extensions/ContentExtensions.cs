// uComponents currently not supported in Umbraco v.7
//using uComponents.DataTypes.DataTypeGrid.Model;
using System;
using System.Collections.Generic;
using umbraco;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;
using Umbraco.Web;

// uComponents currently not supported in Umbraco v.7
//using uComponents.DataTypes.UrlPicker.Dto;

namespace Library.Extensions
{
    public static class ContentExtensions
    {

        /// <summary>
        /// Gets the string value in a textbox
        /// </summary>
        /// <param name="item">The item containing the property</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The string value in the textbox</returns>
        public static string GetTextFieldValue(this IContent item, string propertyName)
        {
            return item.HasProperty(propertyName) ? item.GetValue<string>(propertyName) : null;
        }

        /// <summary>
        /// Gets the pageTitle field or the nodeName if the prior is empty
        /// </summary>
        /// <param name="item">The item containing the multipicker field</param>
        /// <returns>String value in either field (pagetitle, nodename)</returns>
        public static string TitleOrName(this IContent item)
        {
            return item.GetValue<string>("pageTitle") != null && !String.IsNullOrWhiteSpace(item.GetValue<string>("pageTitle")) ? item.GetValue<string>("pageTitle") : item.Name;
        }
        
        /// <summary>
        /// Gets the list of multipicker item data
        /// </summary>
        /// <param name="item">The item containing the multipicker field</param>
        /// <param name="propertyName">The name of the field containing the multipicker data</param>
        /// <returns>A list of IContent objects</returns>
        public static List<IContent> GetMultiPickerItems(this IContent item, string propertyName)
        {
            try
            {
                // try to process the multi-picker as XML
                var multiPickerProperty = new DynamicXml((string)item.GetValue(propertyName));
                var returnList = new List<IContent>();
                foreach (var nextItem in multiPickerProperty)
                {
                    try
                    {
                        var node = UmbracoContext.Current.Application.Services.ContentService.GetPublishedVersion(Convert.ToInt32(nextItem.InnerText));
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
                var ids = ((string)item.GetValue(propertyName)).Split(',');
                var returnList = new List<IContent>();
                foreach (var nextItem in ids)
                {
                    try
                    {
                        var node = UmbracoContext.Current.Application.Services.ContentService.GetPublishedVersion(Convert.ToInt32(nextItem));
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

        /// <summary>
        /// Gets the friendly URL of this item
        /// </summary>
        /// <param name="item">The item to obtain URL of</param>
        /// <returns>The friendly URL</returns>
        public static string NiceUrl(this IContent item)
        {
            return library.NiceUrl(item.Id);
        }

        /// <summary>
        /// Gets the friendly URL of a content item and removes trailing '/' for use with query strings
        /// </summary>
        /// <param name="item">The item to obtain URL of</param>
        /// <returns>The friendly URL ready for addition of query string params</returns>
        public static string NiceUrlWithQueryParams(this IContent item)
        {
            // little helper to remove trailing / if it is found
            var url = library.NiceUrl(item.Id);
            return url[url.Length - 1] == '/' ? url.Substring(0, url.Length - 1) : url;
        }
    }
}


