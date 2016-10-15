using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using System.Data;
using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;
using GameShop.Contracts.Enumerations;
using Dapper;

namespace GameShop.Data.Repositories
{
    public class AdRepository : Repository, IAdRepository, IAdAsyncRepository
    {
        private List<Ad> _ads;
        private List<PCGame> _pcGames;
        private List<ConsoleGame> _consoleGames;

        public AdRepository(IDatabaseProviderFactory provider) 
            : base(provider)
        {
            _ads = new List<Ad>();

            _pcGames = new List<PCGame>()
            {
                new PCGame() { ProductId = Guid.NewGuid(), ProductName = "Test 1", GameGenre = GameGenre.Action },
                new PCGame() { ProductId = Guid.NewGuid(), ProductName = "Dragon Age 3: Inquisition", GameGenre = GameGenre.RPG },
                new PCGame() { ProductId = Guid.NewGuid(), ProductName = "Witcher 3", GameGenre = GameGenre.RPG },
                new PCGame() { ProductId = Guid.NewGuid(), ProductName = "Mass Effect 4", GameGenre = GameGenre.SciFi },
                new PCGame() { ProductId = Guid.NewGuid(), ProductName = "Torchlight", GameGenre = GameGenre.Simulation, SystemRequirements = new ComputerSpecification() { CPU = new CPU() { Cores = 4, Name = "Intel i7 5700", ClockSpeed = "3.40" } } }
            };
            
            _consoleGames = new List<ConsoleGame>()
            {
                new ConsoleGame() { ProductId = Guid.NewGuid(), ProductName = "PS2 Game", GameGenre = GameGenre.RPG, GamingPlatform = GamingPlatform.Xbox360 },
                new ConsoleGame() { ProductId = Guid.NewGuid(), ProductName = "3DS Game", GameGenre = GameGenre.Simulation, GamingPlatform = GamingPlatform.PlayStation2 }
            };

            var pcGamesAd = new Ad();
            pcGamesAd.Products.AddRange(_pcGames);
            pcGamesAd.Id = Guid.NewGuid();
            pcGamesAd.FriendlyId = "123";
            pcGamesAd.Title = "PC Games For Sale!";
            pcGamesAd.Description = "Test Description For PC Games Ad.";

            var consoleGamesAd = new Ad();
            consoleGamesAd.Products.AddRange(_consoleGames);
            consoleGamesAd.Id = Guid.NewGuid();
            consoleGamesAd.FriendlyId = "321";
            consoleGamesAd.Title = "Console Games For Sale!";
            consoleGamesAd.Description = "Test Description For Console Games Ad.";

            _ads.Add(pcGamesAd);
            _ads.Add(consoleGamesAd);
        }

        public Ad FindById(Guid id)
        {
            return _ads.FirstOrDefault(p => p.Id == id);
        }

        public Ad FindByFriendlyId(string id)
        {
            return _ads.FirstOrDefault(p => p.FriendlyId == id);
        }

        public IEnumerable<Ad> FindByTitle(string title)
        {
            return _ads.FindAll(ad => ad.Title.Contains(title));
        }

        public IEnumerable<Ad> GetAll()
        {
            return _ads;
        }

        public int DeleteById(Guid id)
        {
            if(_ads.Remove(_ads.FirstOrDefault(p => p.Id == id)))
            {
                return 1;
            }

            return 0;
        }

        public async Task<Ad> FindByIdAsync(Guid id)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QuerySingleOrDefaultAsync<Ad>($"SELECT * FROM { TableName } WHERE Id LIKE @Id", new { Id = id }).ConfigureAwait(false);
            }
        }

        public async Task<Ad> FindByFriendlyIdAsync(string id)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QuerySingleOrDefaultAsync<Ad>($"SELECT * FROM { TableName } WHERE FriendlyId LIKE @FriendlyId", new { FriendlyId = id }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Ad>> FindByTitleAsync(string title)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.QueryAsync<Ad>($"SELECT * FROM { TableName } WHERE Title LIKE @Title", new { Title = title }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Ad>> GetAllAsync()
        {
            return await Task.Run<IEnumerable<Ad>>(() =>
            {
                return GetAll();
            });

            //using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            //{
            //    return await databaseConnection.QueryAsync<Ad>($"SELECT * FROM { TableName }").ConfigureAwait(false);
            //}
        }

        public async Task<int> DeleteByIdAsync(Guid id)
        {
            using (var databaseConnection = DatabaseProviderFactory.CreateConnection())
            {
                return await databaseConnection.ExecuteAsync($"DELETE FROM { TableName } WHERE Id LIKE @Id", new { Id = id }).ConfigureAwait(false);
            }
        }
    }
}
