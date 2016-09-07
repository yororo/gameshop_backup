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
    public interface IAdAsyncRepository
    {
        Task<Ad> FindByIdAsync(Guid id);
        Task<Ad> FindByFriendlyIdAsync(string id);
        Task<IEnumerable<Ad>> FindByTitleAsync(string title);
        Task<IEnumerable<Ad>> GetAllAsync();
        Task<int> DeleteByIdAsync(Guid id);
    }

    /// <summary>
    /// Ad Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IAdAsyncRepository<TId>
    {
        Task<Ad> FindByIdAsync(TId id);
        Task<Ad> FindByFriendlyIdAsync(string id);
        Task<IEnumerable<Ad>> FindByTitleAsync(string title);
        Task<IEnumerable<Ad>> GetAllAsync();
        Task<int> DeleteByIdAsync(TId id);
    }
}
