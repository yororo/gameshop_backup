using GameShop.Contracts;
using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Services.GameShopApis.Interfaces
{
    public interface IGameShopAdsApi
    {
        Task<IEnumerable<Advertisement>> GetAllAdsAsync();
        Task<Advertisement> GetAdByIdAsync(Guid id);
        Task<Advertisement> GetAdByFriendlyIdAsync(string friendlyId);
        Task<IEnumerable<Advertisement>> FindAdsByTitleAsync(string title);
    }
}
