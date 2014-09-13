using System;
using System.Xml;
using Umbraco.Core.Models;

namespace Library.Extensions
{
    public static class MediaExtensions
    {
        /// <summary>
        /// Gets the full relative URL of the selected media item
        /// </summary>
        /// <param name="item">The media item</param>
        /// <returns>A String containing the relative path of the media item</returns>
        public static string GetMediaURL(this IMedia item)
        {
            var mediaId = item.Id;
            try
            {
                var media = umbraco.library.GetMedia(Convert.ToInt32(mediaId), false);
                var _xmlNode = ((IHasXmlNode)media.Current).GetNode().FirstChild;
                return _xmlNode.FirstChild.InnerText;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
