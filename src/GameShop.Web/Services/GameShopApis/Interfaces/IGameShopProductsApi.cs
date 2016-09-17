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
        Task<IEnumerable<Advertisement>> GetAllProductsAsync();
        Task<Advertisement> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Advertisement>> FindProductsByTitleAsync(string title);
    }
}
