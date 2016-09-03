using GameShop.Interface;
using GameShop.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.GameShopApis.Interfaces
{
    public interface IGameShopProductsApi
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> FindProductsByTitleAsync(string title);
    }
}
