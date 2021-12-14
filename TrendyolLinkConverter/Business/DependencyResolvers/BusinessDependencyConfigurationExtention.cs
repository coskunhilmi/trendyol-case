using Business.Abstract;
using Business.Concrete;
using DataAccess.EntityFramework.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
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
}
