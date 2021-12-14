using Entities.Requests;
using Entities.Responses;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILinkService
    {
        ResponseWrapper<UrlToDeepLinkDto> ConvertWebUrlToDeepLink(UrlToDeepLinkRequest urlToDeepLinkRequest);
        ResponseWrapper<DeepLinkToUrlDto> ConvertDeepLinkToWebUrl(DeepLinkToUrlRequest deepLinkToUrlRequest);
    }
}
