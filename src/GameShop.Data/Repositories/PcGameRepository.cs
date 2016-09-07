using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GameShop.Data.Providers.Interfaces;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;

namespace GameShop.Data.Repositories
{
    public class PcGameRepository : Repository, IPcGameAsyncRepository
    {
        public PcGameRepository(IDatabaseProviderFactory provider)
            : base(provider)
        {
            TableName = "PCGames";
        }

        public async Task<IEnumerable<PcGame>> GetAllAsync()
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QueryAsync<PcGame>($"SELECT * FROM { TableName }");
            }
        }

        public async Task<IEnumerable<PcGame>> GetByNameAsync(string name)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QueryAsync<PcGame>("SELECT * FROM PCGames WHERE Name LIKE @Name", new { Name = name });
            }
        }

        public async Task<IEnumerable<PcGame>> GetByGenreAsync(GameGenre gameGenre)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QueryAsync<PcGame>("SELECT * FROM PCGames WHERE GameGenre LIKE @GameGenre", new { GameGenre = gameGenre });
            }
        }

        public async Task<PcGame> GetByIdAsync(Guid id)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QuerySingleOrDefaultAsync<PcGame>($"SELECT * FROM { TableName } WHERE Id LIKE @Id", new { Id = id });
            }
        }
    }
}
