// uComponents currently not supported in Umbraco v.7
//using uComponents.DataTypes.UrlPicker.Dto;

namespace Library.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates the source string and appends a suffix
        /// </summary>
        /// <param name="source">The string containing original text</param>
        /// <param name="length">The resulting length value</param>
        /// <param name="suffix">The suffix to append</param>
        /// <returns>truncated string value of the original text</returns>
        public static string Truncate(this string source, int length, string suffix)
        {
            if (source.Length > length)
                source = source.Substring(0, length - suffix.Length) + suffix;

            return source;
        }
    }
}
