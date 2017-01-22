using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using GameShop.Data.Contracts;
using GameShop.Data.EF;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Entities;
using GameShop.Data.EF.Repositories;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameShopRepositories(this IServiceCollection services, string connectionString)
        {
            // Register GameShop context.
            services.AddDbContext<GameShopContext>(options => options.UseMySql(connectionString));

            // Register repositories.
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IGameAdvertisementRepository, GameAdvertisementRepository>();

            return services;
        }
    }
}
