using GameShop.Web.Services.GameShopApis;
using GameShop.Web.Services.GameShopApis.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.Extensions
{
    public static class GameShopApiExtensions
    {
        public static void AddGameShopApis(this IServiceCollection services)
        {
            services.AddSingleton<IGameShopProductsApi, GameShopProductsApi>();
            services.AddSingleton<IGameShopApi, GameShopApi>();
        }
    }
}
