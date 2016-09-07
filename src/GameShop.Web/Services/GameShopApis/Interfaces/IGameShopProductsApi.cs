using GameShop.Contracts;
using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.GameShopApis.Interfaces
{
    public interface IGameShopProductsApi
    {
        Task<IEnumerable<Ad>> GetAllProductsAsync();
        Task<Ad> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Ad>> FindProductsByTitleAsync(string title);
    }
}
