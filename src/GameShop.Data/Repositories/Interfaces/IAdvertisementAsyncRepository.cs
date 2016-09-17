using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Ad Async Repository which uses <typeparamref name="Guid"/> as default type for ID.
    /// </summary>
    public interface IAdvertisementAsyncRepository : IAdvertisementAsyncRepository<Guid>
    {
    }

    /// <summary>
    /// Ad Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IAdvertisementAsyncRepository<TId>
    {
        Task<Advertisement> FindByIdAsync(TId id);
        Task<Advertisement> FindByFriendlyIdAsync(string id);
        Task<IEnumerable<Advertisement>> FindByTitleAsync(string title);
        Task<IEnumerable<Advertisement>> GetAllAsync();
        Task<int> AddAsync(Advertisement advertisement);
        Task<int> UpdateAsync(TId id, Advertisement advertisement);
        Task<int> DeleteByIdAsync(TId id);
    }
}
