using GameShop.Data.Contracts;
using GameShop.Data.EF;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameShopContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GameShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddGameShopRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
