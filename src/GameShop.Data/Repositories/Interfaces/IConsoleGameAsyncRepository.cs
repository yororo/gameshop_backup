using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Console Game Async Repository which uses <typeparamref name="Guid"/> as default type for ID.
    /// </summary>
    public interface IConsoleGameAsyncRepository : IProductAsyncRepository<ConsoleGame, Guid>
    {

    }

    /// <summary>
    /// Console Game Async Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IConsoleGameAsyncRepository<TId> : IProductAsyncRepository<ConsoleGame, TId>
    {

    }
}
