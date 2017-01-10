using GameShop.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Providers.Interfaces;
using Dapper;
using System.Data;

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
        public async Task<int> AddGameAsync(Game game)
        {
            string spAddGame = @"spAddGame";

            var parameters = new DynamicParameters();
            parameters.Add(@"GameId", game.Id, dbType: DbType.Guid);
            parameters.Add(@"AdvertisementId", new Guid(), dbType: DbType.Guid);
            parameters.Add(@"SellingInformationId", game.SellingInformation.SellingInformationId, dbType: DbType.Guid);
            parameters.Add(@"TradingInformationId", game.TradingInformation.TradingInformationId, dbType: DbType.Guid);
            parameters.Add(@"Title", game.Name, dbType: DbType.String);
            parameters.Add(@"Description", game.Description, dbType: DbType.String);
            parameters.Add(@"GameGenre", game.GameGenre, dbType: DbType.Int16);
            parameters.Add(@"GamePlatform", game.GamingPlatform, dbType: DbType.Int16);
            parameters.Add(@"State", game.ProductState, dbType: DbType.Int16);
            parameters.Add(@"CreatedDate", game.CreatedDate, dbType: DbType.DateTime2);
            parameters.Add(@"ModifiedDate", game.ModifiedDate, dbType: DbType.DateTime2);

            using (var databaseConnection = Client.CreateConnection())
            {
                return await databaseConnection.ExecuteAsync(
                    spAddGame, 
                    parameters, 
                    commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

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
