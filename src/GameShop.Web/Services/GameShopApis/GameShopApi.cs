using GameShop.Web.Services.GameShopApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.GameShopApis
{
    public class GameShopApi : IGameShopApi
    {
        public GameShopApi(IGameShopAdsApi productsApi)
        {
            Products = productsApi;
        }

        public IGameShopAdsApi Products { get; }
    }
}
