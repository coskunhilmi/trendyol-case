

# DeepLink to Url, Url to Deeplink Converter Web API For Trendyol Hiring Test Case

**TrendyolLinkConverter Web API is built for converting deeplink and url to eachother.**

## Technologies and Packages
- C#
- .Net 5.0
- PostgreSQL 13.4
- Visual Studio 2019
- Microsoft.EntityFrameworkCore.Tools 5.0.10
- Npgsql.EntityFrameworkCore.PostgreSQL 5.0.10
- Npgsql.EntityFrameworkCore.PostgreSQL.Design 1.1.0
- Swashbuckle.AspNetCore 5.6.3
- Newtonsoft.Json 13.0.1
- MSTest.TestFramework 2.2.3
- Moq 4.16.1

## Features
- Code First Approach
- NTier Architecture
- Test Driven Development (TDD) Approach
## Overview

```c#
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
    //This endpoint converts given url to valid deeplink
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
    //This endpoint converts given deeplink to valid trendyol link
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
```

## Table of Contents

  * [Installation](#installation)
     * [Git clone](#git-clone)
     * [Open project and ready to run](#open-project)
  * [Project Structure](#project-structure)
     * [Business Layer](#business-layer) 
     * [Business.Test layer](#business-test-layer)
     * [DataAccess layer](#dataaccess-layer)
     * [Entities layer](#entities-layer)
     * [WebAPI layer](#webapi-layer)
## Installation
### Git clone
```bash
git clone https://github.com/DevelopmentHiring/TrendyolCase-YasinAksu.git
```
### Open project
- {your local path}\TrendyolCase-YasinAksu\TrendyolLinkConverter find TrendyolLinkConverter.sln file double click to open with Visual Studio 2019
- Build solution to installing dependencies
- You can find "application.json" file under WebAPI root directory and change database connection string according to your database connection settings
```js
"ConnectionStrings": {
"DefaultConnection": "User ID=postgres;Password=docker;Server=localhost;Port=5432;Database=trendyoldb"
}
```
- Open "Package Manager Console" on Visual Studio 2019 and make sure you choose "DataAccess" class library as Default project from combobox and than run the following command to apply the lastest migration to database to creating tables
```bash
update-database
```
- Right click on the WebAPI project and than select "Set as Startup Project" in the drop down menu
- Finally run project if you see swagger ui on your browser with this address (https://localhost:44311/swagger/index.html) well done you successfully run the project.
## Project structure
All Layers communicate with each other through interfaces in accordance with the Dependency Inversion principle of 'SOLID'. Built-in .net Ioc container used for dependency injection.
### Business layer
- Business layer consists of 6 main folders
  - Abstract folder contains interfaces ILinkService (for converting url and deeplink to each other), IRequestService(to insert every request object to database table of "requests") and IResponseService (to insert every response object to "responses" table)
    ```c#
        public interface IRequestService
        {
            void Add(Request request);
        }
    ```
  - Concrete folder contains implementations of Interfaces on the Abstract folder.
    ```c#
        public class RequestService : IRequestService
        {
            private readonly TrendyolDbContext _context;
            public RequestService(TrendyolDbContext context)
            {
                _context = context;
            }

            public void Add(Request request)
            {
                _context.Requests.Add(request);
                _context.SaveChanges();
            }
        }
    ```
  - CustomExceptions folder contains custom exception classes for handling exception and managing application exception by the flow
    ```c#
            public class WrongDomainAddressException : Exception
            {
                public WrongDomainAddressException(string message):base(message)
                {

                }
            }
    ```
  - DependecyResolvers contains IServiceCollection extention metod to inject dependencies of the applications
    ```c#
            public static class BusinessDependencyConfigurationExtention
            {
                public static void AddBusinessDependencyInjectionModule(this IServiceCollection services)
                {
                    services.AddSingleton<ILinkService, LinkService>();
                    services.AddSingleton<TrendyolDbContext, TrendyolDbContext>();
                    services.AddSingleton<IRequestService, RequestService>();
                    services.AddSingleton<IResponseService, ResponseService>();
                }
            }
    ```
  - Extentions folder contains global ExceptionHandling middleware to managing application work flow and what happen when application fired any exception.
  - Helper folders contain some static helper metot for clean coding and reusability for convertin url and deeplink to each other.
    
  
  ### Business Test Layer
  This layer contains LinkService.cs test case when converting url to deeplink or deeplink to url by different case
  ```c#
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
        }
    ```

  ### DataAccess Layer
  This layer contains migration and entityframework fluent api mapping configuration and context for managing database tables by orm

  ### Entities Layer
  In this layer, there are entity classes corresponding to database tables, dto classes, and Request and Response classes that meet the requests sent to the API and the responses returned from the API.

  ### WebAPI Layer
  WebAPI layer contains endpoints to communicate client and server through http. 
