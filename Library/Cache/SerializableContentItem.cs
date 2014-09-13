using System.Runtime.Serialization;
using Library.Extensions;
using Library.Shared;
using Umbraco.Core.Models;

namespace Library.Cache
{
    [DataContract]
    public class SerializableContentItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string URL { get; set; }

        [DataMember]
        public string TitleOrName { get; set; }

        [DataMember]
        public string NiceUrlWithQueryParams { get; set; }

        public SerializableContentItem(IContent contentItem)
        {
            Id = contentItem.Id;
            URL = contentItem.NiceUrl();

            TitleOrName = contentItem.Name;

            if (contentItem.Properties.Contains(UmbracoAliases.PageTitle))
            {
                var title = contentItem.GetValue(UmbracoAliases.PageTitle).ToString();

                if (!string.IsNullOrWhiteSpace(title))
                    TitleOrName = title;
            }

            // set the Nice Url With Querystring Params by removing the trailing '/' if it exists
            NiceUrlWithQueryParams = contentItem.NiceUrlWithQueryParams();
        }
    }
}
