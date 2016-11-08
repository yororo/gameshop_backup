using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Advertisement Async Repository whose Advertisement ID is of type Guid.
    /// </summary>
    /// <typeparam name="TProduct">Type of products.</typeparam>
    public interface IAdvertisementAsyncRepository<TProduct> : IAdvertisementAsyncRepository<Guid, TProduct> where TProduct : Product
    {

    }

    /// <summary>
    /// Advertisement Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    /// <typeparam name="TProduct">Type of products.</typeparam>
    public interface IAdvertisementAsyncRepository<TId, TProduct> where TProduct : Product
    {
        /// <summary>
        /// Find advertisement by ID.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Advertisements.</returns>
        Task<Advertisement<TProduct>> FindByIdAsync(TId advertisementId);

        /// <summary>
        /// Find advertisements by friendly ID.
        /// </summary>
        /// <param name="friendlyId">Friendly advertisement ID.</param>
        /// <returns></returns>
        Task<Advertisement<TProduct>> FindByFriendlyIdAsync(string friendlyId);

        /// <summary>
        /// Find advertisements by title.
        /// </summary>
        /// <param name="advertisementTitle">Advertisement title.</param>
        /// <returns>Advertisements.</returns>
        Task<IEnumerable<Advertisement<TProduct>>> FindByTitleAsync(string advertisementTitle);

        /// <summary>
        /// Get all advertisements asynchronously.
        /// </summary>
        /// <returns>Advertisements.</returns>
        Task<IEnumerable<Advertisement<TProduct>>> GetAllAsync();

        /// <summary>
        /// Deep get all advertisements asynchronously.
        /// </summary>
        /// <returns>Advertisements.</returns>
        Task<IEnumerable<Advertisement<TProduct>>> GetAllDeepAsync();

        /// <summary>
        /// Get meetup locations specified in the advertisement.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Meetup locations.</returns>
        Task<IEnumerable<Address>> GetMeetupLocationsAsync(TId advertisementId);

        /// <summary>
        /// Get all products associated with the advertisement.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Products.</returns>
        Task<IEnumerable<TProduct>> GetProductsAsync(TId advertisementId);

        /// <summary>
        /// Get advertisement owner.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>User who owns the advertisement.</returns>
        Task<User> GetAdOwnerAsync(TId advertisementId);

        /// <summary>
        /// Add an advertisement to the repository.
        /// </summary>
        /// <param name="advertisement">Advertisement.</param>
        /// <returns>Number of database rows affected.</returns>
        Task<int> AddAsync(Advertisement<TProduct> advertisement);

        /// <summary>
        /// Update an advertisement in the repository.
        /// </summary>
        /// <param name="advertisementId">ID if the advertisement to be updated.</param>
        /// <param name="advertisement">Updated advertisement.</param>
        /// <returns>Number of database rows affected.</returns>
        Task<int> UpdateAsync(TId advertisementId, Advertisement<TProduct> advertisement);

        /// <summary>
        /// Delete advertisement.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Number of database rows affected.</returns>
        Task<int> DeleteByIdAsync(TId advertisementId);
    }
}
