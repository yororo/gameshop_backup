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
    public interface IAdvertisementRepository<TProduct> : IAdvertisementRepository<Guid, TProduct> where TProduct : Product
    {

    }

    /// <summary>
    /// Ad Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    /// <typeparam name="TProduct">Type of products.</typeparam>
    public interface IAdvertisementRepository<TId, TProduct> where TProduct : Product
    {
        Task<Advertisement<TProduct>> FindByIdAsync(TId id);
        Task<Advertisement<TProduct>> FindByFriendlyIdAsync(string id);
        Task<IEnumerable<Advertisement<TProduct>>> FindByTitleAsync(string title);
        Task<IEnumerable<Advertisement<TProduct>>> GetAllAsync();
        Task<IEnumerable<Advertisement<TProduct>>> GetAllDeepAsync();
        Task<IEnumerable<Address>> GetMeetupLocationsAsync(TId id);
        Task<IEnumerable<TProduct>> GetProductsAsync(TId id);
        Task<User> GetOwnerAsync(TId id);
        Task<int> AddAsync(Advertisement<TProduct> advertisement);
        Task<int> UpdateAsync(TId id, Advertisement<TProduct> advertisement);
        Task<int> DeleteByIdAsync(TId id);
    }
}
