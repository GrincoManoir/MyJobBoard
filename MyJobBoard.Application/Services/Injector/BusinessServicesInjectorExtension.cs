using Microsoft.Extensions.DependencyInjection;
using MyJobBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Services.Injector
{
    public static class BusinessServicesInjectorExtension
    {

        /// <summary>
        /// Add to dependency injection services collection all the services required for MyJobBoard business management
        /// </summary>
        /// <param name="servicesCollection"></param>
        public static void AddBusinessServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IDocumentService, DocumentService>()
                .AddScoped<IOpportunityService, OpportunityService>()
                .AddScoped<IUserService, UserService>()
                .AddSingleton<ITokenService,TokenService>();
        }
    }
}
