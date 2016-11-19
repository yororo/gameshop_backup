using GameShop.Web.Services.GameShop;
using GameShop.Web.Services.GameShop.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.Extensions
{
    /// <summary>
    /// Extension for ASP.NET Core IServiceCollection.
    /// </summary>
    public static class GameShopApiExtensions
    {
        /// <summary>
        /// Add GameShop API services to the DI container.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        public static void AddGameShopApis(this IServiceCollection services)
        {
            services.AddSingleton<IGameShopAdvertisementsApi, GameShopAdvertisementsApi>();
        }
    }
}
