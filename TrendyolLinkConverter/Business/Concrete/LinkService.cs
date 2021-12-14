using Business.Abstract;
using Business.CustomExceptions;
using Business.Helpers;
using Entities.Requests;
using Entities.Responses;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccess.EntityFramework.Contexts;
using Entities.Domains;
using System.Collections.Specialized;

namespace Business.Concrete
{
    public class LinkService : ILinkService
    {     

        public ResponseWrapper<DeepLinkToUrlDto> ConvertDeepLinkToWebUrl(DeepLinkToUrlRequest deepLinkToUrlRequest)
        {
            if (deepLinkToUrlRequest == null)
            {
                throw new ArgumentNullException();
            }

            this.ValidateDeepLink(deepLinkToUrlRequest.DeepLink);            

            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "www.trendyol.com";
            var deepLinkUri = new Uri(deepLinkToUrlRequest.DeepLink);
            var queryString = HttpUtility.ParseQueryString(deepLinkUri.Query);
            if (queryString["Page"] == "Product")
            {                
                var productDetailUrl = UrlDeepLinkHelper.ConvertProductDetailDeepLinkToUrl(uriBuilder, deepLinkUri);               
                return new ResponseWrapper<DeepLinkToUrlDto>(
                    true,
                    "DeepLink Web Url e başarıyla çevrildi",
                    new DeepLinkToUrlDto { Url = productDetailUrl });
            }
            if(queryString["Page"] == "Search")
            {
                var searchUrl = UrlDeepLinkHelper.ConvertSearchDeepLinkToUrl(uriBuilder, deepLinkUri);              
                return new ResponseWrapper<DeepLinkToUrlDto>(
                    true,
                    "DeepLink Web Url e başarıyla çevrildi",
                    new DeepLinkToUrlDto { Url = searchUrl });
            }
            var otherPagesUrl = uriBuilder.Uri.AbsoluteUri.Remove(uriBuilder.Uri.AbsoluteUri.LastIndexOf('/'), 1);            
            return new ResponseWrapper<DeepLinkToUrlDto>(
                   true,
                   "DeepLink Web Url e başarıyla çevrildi",
                   new DeepLinkToUrlDto { Url = otherPagesUrl });            
        }

        public ResponseWrapper<UrlToDeepLinkDto> ConvertWebUrlToDeepLink(UrlToDeepLinkRequest urlToDeepLinkRequest)
        {
            if (urlToDeepLinkRequest == null)
            {
                throw new ArgumentNullException();
            }

            var uri = new Uri(urlToDeepLinkRequest.Url, UriKind.Absolute);
            this.ValidateUrl(uri);           

            var deepLink = "ty://?Page=";
            if (uri.AbsolutePath.Contains("-p-"))
            {
                deepLink = UrlDeepLinkHelper.ConvertUrlToProductDetailDeepLink(uri);
                
                return new ResponseWrapper<UrlToDeepLinkDto>(
                    success: true,
                    message: "Url başarı ile deeplinke dönüştürüldü",
                    data: new UrlToDeepLinkDto { DeepLink = deepLink });
            }
            if (uri.AbsolutePath.ToLower().Contains("/sr"))
            {
                deepLink = UrlDeepLinkHelper.ConvertUrlToSearchDeepLink(uri);
               
                return new ResponseWrapper<UrlToDeepLinkDto>(
                    success: true,
                    message: "Url başarı ile deeplinke dönüştürüldü",
                    data: new UrlToDeepLinkDto { DeepLink = deepLink });
            }

            deepLink += "Home";
            
            return new ResponseWrapper<UrlToDeepLinkDto>(
                    success: true,
                    message: "Url başarı ile deeplinke dönüştürüldü",
                    data: new UrlToDeepLinkDto { DeepLink = deepLink });

        }

        public void ValidateUrl(Uri uri)
        {
            var urlIsTrendyol = uri.Host.Contains("www.trendyol.com");
            if (urlIsTrendyol == false)
            {
                throw new WrongDomainAddressException("Verilen domain adresi 'www.trendyol.com' olmalı");
            }
            var scheme = uri.Scheme;
            if (scheme != "https")
            {
                throw new WrongDomainAddressException("Lütfen ssl sertifikalı url gönderiniz");
            }
        }

        public void ValidateDeepLink(string deepLink)
        {
            var urlIsCorrectDeepLink = deepLink.StartsWith("ty://?Page=");
            if (urlIsCorrectDeepLink == false)
            {
                throw new WrongDeepLinkFormatException("Verilen deeplink 'ty://?Page=' ile başlamalı");
            }
            urlIsCorrectDeepLink = deepLink.Contains("boutiqueId") || deepLink.Contains("BoutiqueId");
            if (urlIsCorrectDeepLink == true)
            {
                throw new WrongDeepLinkFormatException("Deeplink boutiqueId içeremez");
            }
        }
    }
}
