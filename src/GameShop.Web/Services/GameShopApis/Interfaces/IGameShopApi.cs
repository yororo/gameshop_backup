using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Website.Services.GameShop.Interfaces
{
    public interface IGameShopApi
    {
        IGameShopAdsApi Products { get; }
    }
}
