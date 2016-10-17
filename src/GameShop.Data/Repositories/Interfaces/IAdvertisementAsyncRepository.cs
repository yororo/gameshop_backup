using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Ad Async Repository.
    /// </summary>
    /// <typeparam name="TProduct">Type of products.</typeparam>
    public interface IAdvertisementAsyncRepository<TProduct> : IAdvertisementAsyncRepository<Guid, TProduct> where TProduct : Product
    {

    }

    /// <summary>
    /// Ad Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    /// <typeparam name="TProduct">Type of products.</typeparam>
    public interface IAdvertisementAsyncRepository<TId, TProduct> where TProduct : Product
    {
        Task<Advertisement> FindByIdAsync(TId id);
        Task<Advertisement> FindByFriendlyIdAsync(string id);
        Task<IEnumerable<Advertisement>> FindByTitleAsync(string title);
        Task<IEnumerable<Advertisement>> GetAllAsync();
        Task<IEnumerable<Advertisement>> GetAllDeepAsync();
        Task<IEnumerable<Address>> GetMeetupLocationsAsync(TId id);
        Task<IEnumerable<TProduct>> GetProductsAsync(TId id);
        Task<User> GetAdOwnerAsync(TId id);
        Task<int> AddAsync(Advertisement advertisement);
        Task<int> UpdateAsync(TId id, Advertisement advertisement);
        Task<int> DeleteByIdAsync(TId id);
    }
}
