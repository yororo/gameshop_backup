using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Providers.Interfaces;

namespace GameShop.Data.Repositories
{
    public class GameRepository : Repository, IGameRepository<Guid>
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="databaseProvider">The Database Provider (MSSQL/MYSQL/etc.)</param>
        public GameRepository(IDatabaseProviderClient databaseProvider)
            : base (databaseProvider)
        { }
        #endregion Constructor

        #region Methods
        public Task<IEnumerable<Game>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetByGenreAsync(GameGenre genre)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        #endregion Methods
    }
}
