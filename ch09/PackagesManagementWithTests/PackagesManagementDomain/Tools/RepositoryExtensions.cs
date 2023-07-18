using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.DomainLayer;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.DomainLayer
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddAllRepositories
            (this IServiceCollection service, Assembly assembly)
        {
            var repositories = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(IRepository).IsAssignableFrom(x));
            foreach (var repository in repositories)
            {
                var repositoryInterface = repository.GetInterfaces()
                    .Where(i => !i.IsGenericType && typeof(IRepository) != i 
                            && typeof(IRepository).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (repositoryInterface != null)
                {
                    service.AddScoped(repositoryInterface, repository);
                }
            }
            return service;
        }
    }
}
