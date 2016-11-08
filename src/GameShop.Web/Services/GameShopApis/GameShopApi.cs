using GameShop.Website.Services.GameShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Website.Services.GameShop
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
