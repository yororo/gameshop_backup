using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.GameShopApis.Interfaces
{
    public interface IGameShopApi
    {
        IGameShopProductsApi Products { get; }
    }
}
