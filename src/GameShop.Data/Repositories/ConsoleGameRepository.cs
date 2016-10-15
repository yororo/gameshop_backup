using Dapper;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    public class ConsoleGameRepository : Repository, IConsoleGameAsyncRepository
    {
        public ConsoleGameRepository(IDatabaseProviderFactory provider)
            : base(provider)
        {
            GamingPlatform = GamingPlatform.PlayStation4;
        }

        public async Task<IEnumerable<ConsoleGame>> GetAllAsync()
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                //return await databaseConnection.QueryAsync<ConsoleGame>($"SELECT * FROM Game WHERE Platform = @Platform", GamingPlatform);
                return await databaseConnection.QueryAsync<ConsoleGame>("spGetAll", new { GamingPlatform }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ConsoleGame>> GetByNameAsync(string name)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QueryAsync<ConsoleGame>("SELECT * FROM Game WHERE Name LIKE @Name AND Platform = @Platform", new { Name = name });
            }
        }

        public async Task<IEnumerable<ConsoleGame>> GetByGenreAsync(GameGenre gameGenre)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                //return await databaseConnection.QueryAsync<ConsoleGame>("SELECT * FROM ConsoleGames WHERE GameGenre LIKE @GameGenre", new { GameGenre = gameGenre });
                return await databaseConnection.QueryAsync<ConsoleGame>("spGetByGenre", new { GameGenre = gameGenre, GamingPlatform = GamingPlatform }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ConsoleGame> GetByIdAsync(Guid id)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                //return await databaseConnection.QuerySingleOrDefaultAsync<ConsoleGame>($"SELECT * FROM { TableName } WHERE Id LIKE @Id", new { Id = id });
                return await databaseConnection.QuerySingleOrDefaultAsync<ConsoleGame>("spGetById", new { GameId = id}, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
