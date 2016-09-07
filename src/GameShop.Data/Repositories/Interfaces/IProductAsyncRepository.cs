using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Product Async Repository which uses Guid as default type for ID.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public interface IProductAsyncRepository<T> where T : Product
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByNameAsync(string name);
        Task<IEnumerable<T>> GetByGenreAsync(GameGenre genre);
        Task<T> GetByIdAsync(Guid id);
    }

    /// <summary>
    /// Product Async Repository.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IProductAsyncRepository<T, TId> where T : Product
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByNameAsync(string name);
        Task<IEnumerable<T>> GetByGenreAsync(GameGenre genre);
        Task<T> GetByIdAsync(TId id);
    }
}
