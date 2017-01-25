using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;

namespace GameShop.Data.Contracts
{
    /// <summary>
    /// Advertisement Async Repository whose Advertisement ID is of type Guid and product type is Product.
    /// </summary>
    public interface IAdvertisementRepository : IAdvertisementRepository<Advertisement, Product>
    {

    }

    /// <summary>
    /// Advertisement Async Repository whose Advertisement ID is of type Guid.
    /// </summary>
    /// <typeparam name="TProduct">Type of products.</typeparam>
    public interface IAdvertisementRepository<TAdvertisement, TProduct> : IAdvertisementRepository<Guid, TAdvertisement, TProduct> where TAdvertisement : Advertisement where TProduct : Product
    {

    }

    /// <summary>
    /// Advertisement Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    /// <typeparam name="TAdvertisement">Type of products.</typeparam>
    public interface IAdvertisementRepository<TId, TAdvertisement, TProduct> where TAdvertisement : Advertisement where TProduct : Product
    {
        /// <summary>
        /// Find advertisement by ID.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Advertisements.</returns>
        Task<TAdvertisement> GetByIdAsync(TId advertisementId);

        /// <summary>
        /// Find advertisements by friendly ID.
        /// </summary>
        /// <param name="friendlyId">Friendly advertisement ID.</param>
        /// <returns></returns>
        Task<TAdvertisement> GetByFriendlyIdAsync(string friendlyId);

        /// <summary>
        /// Find advertisements by title.
        /// </summary>
        /// <param name="advertisementTitle">Advertisement title.</param>
        /// <returns>Advertisements.</returns>
        Task<IEnumerable<TAdvertisement>> GetByTitleAsync(string advertisementTitle);

        /// <summary>
        /// Get all advertisements asynchronously.
        /// </summary>
        /// <returns>Advertisements.</returns>
        Task<IEnumerable<TAdvertisement>> GetAllAsync();

        /// <summary>
        /// Deep get all advertisements asynchronously.
        /// </summary>
        /// <returns>Advertisements.</returns>
        Task<IEnumerable<TAdvertisement>> GetAllDeepAsync();

        /// <summary>
        /// Get meetup locations specified in the advertisement.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Meetup locations.</returns>
        Task<MeetupInformation> GetMeetupInformationAsync(TId advertisementId);

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
        Task<int> AddAsync(TAdvertisement advertisement);

        /// <summary>
        /// Update an advertisement in the repository.
        /// </summary>
        /// <param name="advertisementId">ID if the advertisement to be updated.</param>
        /// <param name="advertisement">Updated advertisement.</param>
        /// <returns>Number of database rows affected.</returns>
        Task<int> UpdateAsync(TId advertisementId, TAdvertisement advertisement);

        /// <summary>
        /// Delete advertisement.
        /// </summary>
        /// <param name="advertisementId">Advertisement ID.</param>
        /// <returns>Number of database rows affected.</returns>
        Task<int> DeleteByIdAsync(TId advertisementId);
    }
}
