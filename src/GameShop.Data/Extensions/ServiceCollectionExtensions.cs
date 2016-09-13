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
        /// <summary>
        /// Use MySql as database client.
        /// </summary>
        /// <param name="services">IServiceCollection instance to add the database client service.</param>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns>IServiceCollection instance with the added database client service.</returns>
        public static IServiceCollection UseGameShopMySql(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabaseClient>(new MySqlClient(connectionString));

            return services;
        }

        /// <summary>
        /// Use SQL Server as database client.
        /// </summary>
        /// <param name="services">IServiceCollection instance to add the database client service.</param>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns>IServiceCollection instance with the added database client service.</returns>
        public static IServiceCollection UseGameShopSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabaseClient>(new MsSqlClient(connectionString));

            return services;
        }

        /// <summary>
        /// Use all game shop repositories.
        /// </summary>
        /// <param name="services">IServiceCollection instance to add the repository services.</param>
        /// <returns>IServiceCollection instance with the added repository services.</returns>
        public static IServiceCollection UseGameShopRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAdvertisementRepository, AdvertisementRepository>();
            services.AddSingleton<IAdvertisementAsyncRepository, AdvertisementRepository>();

            return services;
        }
    }
}
