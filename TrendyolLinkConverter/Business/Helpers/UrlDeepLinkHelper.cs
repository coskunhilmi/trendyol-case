using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business.Helpers
{
    public static class UrlDeepLinkHelper
    {
        public static string ConvertUrlToProductDetailDeepLink(Uri uri)
        {
            var deepLink = "ty://?Page=";
            deepLink += "Product&ContentId=";
            var absolutePathArrays = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var contentId = absolutePathArrays.FirstOrDefault(x => x.Contains("-p-")).Split("-p-", StringSplitOptions.RemoveEmptyEntries)[1];
            deepLink += contentId;
            var queryString = uri.Query;
            var queryDictionary = HttpUtility.ParseQueryString(queryString);

            var boutiqueId = queryDictionary["boutiqueId"];
            var merchantId = queryDictionary["merchantId"];

            if (!String.IsNullOrEmpty(boutiqueId))
            {
                deepLink += $"&CampaignId={boutiqueId}";
            }
            if (!String.IsNullOrEmpty(merchantId))
            {
                deepLink += $"&MerchantId={ merchantId}";
            }
            return deepLink;
        }

        public static string ConvertUrlToSearchDeepLink(Uri uri)
        {
            var deepLink = "ty://?Page=Search&Query=";           
            var queryString = uri.Query;
            var queryDictionary = HttpUtility.ParseQueryString(queryString);
            var q = queryDictionary["q"];

            if (!String.IsNullOrEmpty(q))
            {                
                var urlEncoded = System.Net.WebUtility.UrlEncode(q);
                deepLink += $"{urlEncoded}";
            }
            return deepLink;
        }

        public static string ConvertProductDetailDeepLinkToUrl(UriBuilder uriBuilder, Uri deepLinkUri)
        {            
            var queryString = HttpUtility.ParseQueryString(deepLinkUri.Query);
            uriBuilder.Path = $"/brand/name-p-{queryString["ContentId"]}";
            NameValueCollection queryStringBuilder = HttpUtility.ParseQueryString(string.Empty);
            if (queryString["CampaignId"] != null)
            {
                queryStringBuilder.Add("boutiqueId", queryString["CampaignId"]);
            }
            if (queryString["MerchantId"] != null)
            {
                queryStringBuilder.Add("merchantId", queryString["MerchantId"]);
            }
            uriBuilder.Query = queryStringBuilder.ToString();
            return uriBuilder.Uri.AbsoluteUri;
        }

        public static string ConvertSearchDeepLinkToUrl(UriBuilder uriBuilder, Uri deepLinkUri)
        {
            var queryString = HttpUtility.ParseQueryString(deepLinkUri.Query);
            uriBuilder.Path = "/sr";
            NameValueCollection queryStringBuilder = HttpUtility.ParseQueryString(string.Empty);
            if (queryString["Query"] != null)
            {
                queryStringBuilder.Add("q", queryString["Query"]);
            }
            uriBuilder.Query = queryStringBuilder.ToString();
            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
