using umbraco;
using umbraco.interfaces;

namespace Library.Shared
{
    public class UrlPickerContent
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
    }

    public static class Utility
    {
        //private static ImageUtil iUtil = new ImageUtil();

        //public static string GetImageUrl(INode node, string nodePropertyAlias, ImageResizeProperties irProperties, string placeholderImageUrl = null)
        //{
        //    return GetImageUrl(node.GetProperty<string>(nodePropertyAlias), irProperties, placeholderImageUrl);
        //}

        //public static string GetImageUrl(INode node, string nodePropertyAlias, string profileName, string placeholderImageUrl = null)
        //{
        //    return GetImageUrl(node.GetProperty<string>(nodePropertyAlias), profileName, placeholderImageUrl);
        //}

        //public static string GetImageUrl(string mediaId, ImageResizeProperties irProperties, string placeholderImageUrl = null)
        //{
        //    // TODO: Update this property
        //    if (irProperties == null)
        //        return "";

        //    if (string.IsNullOrWhiteSpace(mediaId) && placeholderImageUrl != null)
        //        return placeholderImageUrl;

        //    return iUtil.GetImageUrl(mediaId, irProperties);
        //}

        //public static string GetImageUrl(string mediaId, string profileName, string placeholderImageUrl = null)
        //{
        //    if (string.IsNullOrWhiteSpace(mediaId) && placeholderImageUrl != null)
        //        return placeholderImageUrl;

        //    return iUtil.GetImageUrl(mediaId, profileName);
        //}
    }
}

