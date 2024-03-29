﻿using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Contracts.Products
{
    /// <summary>
    /// This will implement the use of Guid and Product types.
    /// </summary>
    public interface IProductRepository : IProductRepository<Product>
    {

    }

    /// <summary>
    /// Product Async Repository which uses Guid as default type for ID.
    /// </summary>
    /// <typeparam name="TProduct">Product type.</typeparam>
    public interface IProductRepository<TProduct> : IProductRepository<Guid, TProduct> 
        where TProduct : Product
    {

    }

    /// <summary>
    /// Product Async Repository.
    /// </summary>
    /// <typeparam name="TProduct">Object type.</typeparam>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IProductRepository<TId, TProduct> where TProduct : Product
    {
        Task<int> AddAsync(TProduct product);
        Task<IEnumerable<TProduct>> GetAllAsync();
        Task<IEnumerable<TProduct>> GetByNameAsync(string productName);
        Task<TProduct> GetByIdAsync(TId productId);
        Task<int> UpdateAsync(TId productId, TProduct product);
        Task<int> DeleteByIdAsync(TId productId);
    }
}
