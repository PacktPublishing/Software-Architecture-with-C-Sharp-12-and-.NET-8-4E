using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcMicroServiceStore
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<IUnitOfWork,MainDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("GrpcMicroServiceStore")));
            services.AddScoped<IMessageQueue, MessageQueue>();
            services.AddScoped<IDayStatistics, DayStatistics>();
            return services;
        }
    }
}
