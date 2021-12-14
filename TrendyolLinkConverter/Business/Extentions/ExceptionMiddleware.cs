using Business.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extentions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            if (e.GetType() == typeof(UriFormatException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            if (e.GetType() == typeof(WrongDomainAddressException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            if (e.GetType() == typeof(WrongDeepLinkFormatException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
