using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Data.Repositories;

namespace GameShop.Data.Extensions
{
    /// <summary>
    /// ASP.NET Core Service Collection Extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseMySqlClientFactory(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabaseProviderFactory>(new MySqlClientCustomFactory(connectionString));

            return services;
        }

        public static IServiceCollection UseMsSqlClientFactory(this IServiceCollection services, string gameShopDatabaseConnectionString)
        {
            services.AddSingleton<IDatabaseProviderFactory>(new MsSqlClientCustomFactory(gameShopDatabaseConnectionString));

            return services;
        }

        public static IServiceCollection AddGameShopRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAdRepository, AdRepository>();
            services.AddSingleton<IAdAsyncRepository, AdRepository>();

            return services;
        }
    }
}
