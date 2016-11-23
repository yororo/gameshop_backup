using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Contracts;
using GameShop.Data.Repositories;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// ASP.NET Core Service Collection Extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Use Sql Server database client.
        /// </summary>
        /// <param name="services">IServiceCollection instance to add the database client service.</param>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns>IServiceCollection instance with the added database client service.</returns>
        public static IServiceCollection UseGameshopSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabaseProviderClient>(new SqlServerClient(connectionString));

            return services;
        }

        /// <summary>
        /// Use MySql database client.
        /// </summary>
        /// <param name="services">IServiceCollection instance to add the database client service.</param>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns>IServiceCollection instance with the added database client service.</returns>
        public static IServiceCollection UseGameshopMySql(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabaseProviderClient>(new MySqlClient(connectionString));

            return services;
        }

        /// <summary>
        /// Use all game shop repositories.
        /// </summary>
        /// <param name="services">IServiceCollection instance to add the repository services.</param>
        /// <returns>IServiceCollection instance with the added repository services.</returns>
        public static IServiceCollection UseGameShopRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IGameAdvertisementRepository, GameAdvertisementRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
