using Business.Abstract;
using Entities.Domains;
using Entities.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IRequestService _requestService;
        private readonly IResponseService _responseService;
        public LinksController(ILinkService linkService, IResponseService responseService, IRequestService requestService)
        {
            _linkService = linkService;
            _responseService = responseService;
            _requestService = requestService;
        }

        [HttpPost("urlToDeepLink")]
        public IActionResult UrlToDeepLink(UrlToDeepLinkRequest urlToDeepLinkRequest)
        {
            var result = _linkService.ConvertWebUrlToDeepLink(urlToDeepLinkRequest);
            if(result.Success)
            {
                var request = new Request
                {
                    Content = urlToDeepLinkRequest.Url
                };
                _requestService.Add(request);
                var response = new Response
                {
                    RequestId = request.Id,
                    Content = result.Data.DeepLink
                };
                _responseService.Add(response);
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deepLinkToUrl")]
        public IActionResult DeepLinkToUrl(DeepLinkToUrlRequest deepLinkToUrlRequest)
        {
            var result = _linkService.ConvertDeepLinkToWebUrl(deepLinkToUrlRequest);
            if (result.Success)
            {
                var request = new Request
                {
                    Content = deepLinkToUrlRequest.DeepLink
                };
                _requestService.Add(request);
                var response = new Response
                {
                    RequestId = request.Id,
                    Content = result.Data.Url
                };
                _responseService.Add(response);
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
