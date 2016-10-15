﻿using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;

namespace GameShop.Data.Repositories
{
    public class DummyRepository : Repository, IAdAsyncRepository, IPCGameAsyncRepository, IConsoleGameAsyncRepository, IHandheldGameAsyncRepository
    {
        public DummyRepository(IDatabaseProviderFactory provider)
            : base(provider)
        {
        }

        #region IAdAsyncRepository Implementation

        public Task<int> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Ad> FindByFriendlyIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Ad> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ad>> FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ad>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        #endregion IAdAsyncRepository Implementation

        #region IPCGameAsyncRepository Implementation



        public Task<IEnumerable<PCGame>> GetByGenreAsync(GameGenre genre)
        {
            throw new NotImplementedException();
        }

        public Task<PCGame> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PCGame>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ConsoleGame>> IProductAsyncRepository<ConsoleGame, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<HandheldGame>> IProductAsyncRepository<HandheldGame, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PCGame>> IProductAsyncRepository<PCGame, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<HandheldGame>> IProductAsyncRepository<HandheldGame, Guid>.GetByGenreAsync(GameGenre genre)
        {
            throw new NotImplementedException();
        }

        #endregion IPCGameAsyncRepository Implementation

        #region IConsoleGameAsyncRepository Implementation

        Task<IEnumerable<ConsoleGame>> IProductAsyncRepository<ConsoleGame, Guid>.GetByGenreAsync(GameGenre genre)
        {
            throw new NotImplementedException();
        }

        Task<HandheldGame> IProductAsyncRepository<HandheldGame, Guid>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<ConsoleGame> IProductAsyncRepository<ConsoleGame, Guid>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion IConsoleGameAsyncRepository Implementation

        #region IHandheldGameAsyncRepository Implementation

        Task<IEnumerable<HandheldGame>> IProductAsyncRepository<HandheldGame, Guid>.GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ConsoleGame>> IProductAsyncRepository<ConsoleGame, Guid>.GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        #endregion IHandheldGameAsyncRepository Implementation
    }
}
