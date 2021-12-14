using Business.Abstract;
using Business.Concrete;
using Business.CustomExceptions;
using DataAccess.EntityFramework.Contexts;
using Entities.Domains;
using Entities.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Business.Tests
{
    [TestClass]
    public class _linkServiceTest
    {
        private ILinkService _linkService;
        [TestInitialize]
        public void Startup()
        {            
            _linkService = new LinkService();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDomainAddressException))]
        public void ConvertWebUrlToDeepLink_ThrowsException_IfGivenUrlIsNotTrendyol()
        {
            //arrange
            var request = new UrlToDeepLinkRequest { Url = "https://www.google.com.tr" };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void ConvertWebUrlToDeepLink_ThrowsException_IfGivenUrlCannotConvertToUriInstance()
        {
            //arrange
            var request = new UrlToDeepLinkRequest { Url = "this is not able to convert an Uri" };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDomainAddressException))]
        public void ConvertWebUrlToDeepLink_ThrowsException_IfGivenUrlIsNotSecure()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "http://www.trendyol.com/casio/saat-p1925865?boutiqueId=439892&merchantId=105064"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);
            //assert
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsProductDetailDeepLink_IfGivenUrlContainsBoutiqueIdAndMerchantId()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsProductDetailDeepLink_IIfGivenUrlContainsOnlyBoutiqueId()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Product&ContentId=1925865&CampaignId=439892";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsProductDetailDeepLink_IfGivenUrlContainsOnlyMerchantId()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/casio/saat-p-1925865?merchantId=105064"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Product&ContentId=1925865&MerchantId=105064";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsProductDetailDeepLink_IfGivenUrlWithoutQueryParameters()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/casio/saat-p-1925865"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Product&ContentId=1925865";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsSearchPageDeepLink_IfGivenSearchParameterConsistOfSingleWordAndWithoutTurkishCharacters()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/sr?q=elbise"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Search&Query=elbise";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsSearchPageDeepLink_IfGivenSearchParameterConsistOfSingleWordAndWithTurkishCharacters()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/sr?q=%C3%BCt%C3%BC"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Search&Query=%C3%BCt%C3%BC";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertWebUrlToDeepLink_ReturnsEmptyHomePageDeepLink_IfGivenOtherThanSearchOrProductDetail()
        {
            //arrange
            var request = new UrlToDeepLinkRequest
            {
                Url = "https://www.trendyol.com/Hesabim/Favoriler"
            };

            //act
            var result = _linkService.ConvertWebUrlToDeepLink(request);

            //assert
            var expected = "ty://?Page=Home";
            var actual = result.Data.DeepLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDeepLinkFormatException))]
        public void ConvertDeepLinkToWebUrl_ThrowsException_IfGivenDeepLinkIsNotTrendyol()
        {
            //arrange
            var request = new DeepLinkToUrlRequest { DeepLink = "by://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064" };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDeepLinkFormatException))]
        public void ConvertDeepLinkToWebUrl_ThrowsException_IfGivenDeepLinkContainsBoutiqueId()
        {
            //arrange
            var request = new DeepLinkToUrlRequest { DeepLink = "ty://?Page=Product&ContentId=1925865&BoutiqueId=439892&MerchantId=105064" };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);
            //assert
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsProductDetailUrl_IIfGivenUrlContainsOnlyCampaignId()
        {
            //arrange
            var request = new DeepLinkToUrlRequest
            {
                DeepLink= "ty://?Page=Product&ContentId=1925865&CampaignId=439892"
            };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);

            //assert
            var expected = "https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892";
            var actual = result.Data.Url;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsProductDetailUrl_IfGivenUrlContainsOnlyMerchantId()
        {
            //arrange
            var request = new DeepLinkToUrlRequest
            {
                DeepLink = "ty://?Page=Product&ContentId=1925865&MerchantId=105064"
            };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);            

            //assert
            var expected = "https://www.trendyol.com/brand/name-p-1925865?merchantId=105064";
            var actual = result.Data.Url;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsProductDetailUrl_IfGivenUrlWithoutQueryParameters()
        {
            //arrange
            var request = new DeepLinkToUrlRequest
            {
                DeepLink= "ty://?Page=Product&ContentId=1925865"
            };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);

            //assert
            var expected = "https://www.trendyol.com/brand/name-p-1925865";
            var actual = result.Data.Url;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsProductDetailUrl_IfGivenDeepLinkProductDetail()
        {
            //arrange
            var request = new DeepLinkToUrlRequest { DeepLink = "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064" };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);
            //assert
            var expected = "https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892&merchantId=105064";
            var actual = result.Data.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsSearchUrl_IfGivenSearchParameterConsistOfOneWordAndWithoutTurkishCharacters()
        {
            //arrange
            var request = new DeepLinkToUrlRequest
            {
                DeepLink= "ty://?Page=Search&Query=elbise"
            };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);

            //assert
            var expected = "https://www.trendyol.com/sr?q=elbise";
            var actual = result.Data.Url;
            Assert.AreEqual(expected, actual);
            //IfGivenSearchParameterConsistOfSingleWordAndWithTurkishCharacters
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsSearchUrl_IfGivenQueryParamConsistOfSingleWordAndWithTurkishCharacters()
        {
            //arrange
            var request = new DeepLinkToUrlRequest
            {
                DeepLink = "ty://?Page=Search&Query=%C3%BCt%C3%BC"
            };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);

            //assert
            var expected = "https://www.trendyol.com/sr?q=%C3%BCt%C3%BC";
            var actual = result.Data.Url;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertDeepLinkToWebUrl_ReturnsHomePageUrl_IfGivenOtherThanSearchOrProductDetail()
        {
            //arrange
            var request = new DeepLinkToUrlRequest
            {
                DeepLink= "ty://?Page=Favorites"
            };

            //act
            var result = _linkService.ConvertDeepLinkToWebUrl(request);

            //assert
            var expected = "https://www.trendyol.com";
            var actual = result.Data.Url;
            Assert.AreEqual(expected, actual);
        }
    }
}
