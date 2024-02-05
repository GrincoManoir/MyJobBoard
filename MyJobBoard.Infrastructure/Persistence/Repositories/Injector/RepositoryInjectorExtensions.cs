using Microsoft.Extensions.DependencyInjection;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Infrastructure.Persistence.Repositories.Injector
{
    public static class RepositoryInjectorExtensions

    {

        /// <summary>
        /// Add to dependency injection services collection all repositories required for MyJobBoard architecture data management
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddMyJobBoardRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDocumentRepository, DocumentRepository>()
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
