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
    public class AdvertisementRepository : Repository, IAdvertisementRepository, IAdvertisementAsyncRepository
    {
        private List<Advertisement> _ads;
        private List<PCGame> _pcGames;
        private List<ConsoleGame> _consoleGames;

        public AdvertisementRepository(IDatabaseClient provider) 
            : base(provider)
        {
            //Table name.
            TableName = "Advertisements";

            _ads = new List<Advertisement>();

            _pcGames = new List<PCGame>()
            {
                new PCGame() { Id = Guid.NewGuid(), Name = "Test 1", Genre = GameGenre.Action },
                new PCGame() { Id = Guid.NewGuid(), Name = "Dragon Age 3: Inquisition", Genre = GameGenre.RPG },
                new PCGame() { Id = Guid.NewGuid(), Name = "Witcher 3", Genre = GameGenre.RPG },
                new PCGame() { Id = Guid.NewGuid(), Name = "Mass Effect 4", Genre = GameGenre.SciFi },
                new PCGame() { Id = Guid.NewGuid(), Name = "Torchlight", Genre = GameGenre.Simulation, SystemRequirements = new ComputerSpecification() { CPU = new CPU() { Cores = 4, Name = "Intel i7 5700", ClockSpeed = "3.40" } } }
            };


            _consoleGames = new List<ConsoleGame>()
            {
                new ConsoleGame() { Id = Guid.NewGuid(), Name = "PS2 Game", Genre = GameGenre.RPG, Platform = GamingPlatform.Xbox360 },
                new ConsoleGame() { Id = Guid.NewGuid(), Name = "3DS Game", Genre = GameGenre.Simulation, Platform = GamingPlatform.PlayStation2 }
            };


            var pcGamesAd = new Advertisement(_pcGames);
            pcGamesAd.Id = Guid.NewGuid();
            pcGamesAd.FriendlyId = "123";
            pcGamesAd.Title = "PC Games For Sale!";
            pcGamesAd.Description = "Test Description For PC Games Ad.";

            var consoleGamesAd = new Advertisement(_consoleGames);
            consoleGamesAd.Id = Guid.NewGuid();
            consoleGamesAd.FriendlyId = "321";
            consoleGamesAd.Title = "Console Games For Sale!";
            consoleGamesAd.Description = "Test Description For Console Games Ad.";

            _ads.Add(pcGamesAd);
            _ads.Add(consoleGamesAd);

        }

        #region IAdvertisementRepository Implementation

        public Advertisement FindById(Guid id)
        {
            return _ads.FirstOrDefault(p => p.Id == id);
        }

        public Advertisement FindByFriendlyId(string id)
        {
            return _ads.FirstOrDefault(p => p.FriendlyId == id);
        }

        public IEnumerable<Advertisement> FindByTitle(string title)
        {
            return _ads.FindAll(ad => ad.Title.Contains(title));
        }

        public IEnumerable<Advertisement> GetAll()
        {
            return _ads;
        }

        public int Add(Advertisement advertisement)
        {
            _ads.Add(advertisement);

            return 1;
        }

        public int Update(Guid id, Advertisement advertisement)
        {
            var ad = _ads.FirstOrDefault(a => a.Id == id);

            if(_ads.Remove(ad))
            {
                _ads.Add(advertisement);
                return 1;
            }

            return 0;
        }

        public int DeleteById(Guid id)
        {
            if (_ads.Remove(_ads.FirstOrDefault(p => p.Id == id)))
            {
                return 1;
            }

            return 0;
        }

        #endregion

        #region IAdvertisementAsyncRepository Implementation

        public async Task<Advertisement> FindByIdAsync(Guid id)
        {
            using (var databaseConnection = DatabaseClient.CreateConnection())
            {
                return await databaseConnection.QuerySingleOrDefaultAsync<Advertisement>($"SELECT * FROM { TableName } WHERE Id LIKE @Id", new { Id = id }).ConfigureAwait(false);
            }
        }

        public async Task<Advertisement> FindByFriendlyIdAsync(string id)
        {
            using (var databaseConnection = DatabaseClient.CreateConnection())
            {
                return await databaseConnection.QuerySingleOrDefaultAsync<Advertisement>($"SELECT * FROM { TableName } WHERE FriendlyId LIKE @FriendlyId", new { FriendlyId = id }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Advertisement>> FindByTitleAsync(string title)
        {
            using (var databaseConnection = DatabaseClient.CreateConnection())
            {
                return await databaseConnection.QueryAsync<Advertisement>($"SELECT * FROM { TableName } WHERE Title LIKE @Title", new { Title = title }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            using (var databaseConnection = DatabaseClient.CreateConnection())
            {
                return await databaseConnection.QueryAsync<Advertisement>($"SELECT * FROM { TableName }").ConfigureAwait(false);
            }
        }

        public async Task<int> AddAsync(Advertisement advertisement)
        {

            using (var databaseConnection = DatabaseClient.CreateConnection())
            {
                return await databaseConnection.ExecuteAsync(
                    $"INSERT INTO { TableName }(Id, FriendlyId, Title, Description, OwnerID, Created, Modified) VALUES(@Id, @FriendlyId, @Title, @Description, @OwnerID, @Created, @Modified)",
                    new
                    {
                        Id = Guid.Empty,
                        FriendlyId = advertisement.FriendlyId,
                        Title = advertisement.Title,
                        Description = advertisement.Description,
                        OwnerID = advertisement.Owner.Id,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    }
                );
            }
        }

        public async Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteByIdAsync(Guid id)
        {
            using (var databaseConnection = DatabaseClient.CreateConnection())
            {
                return await databaseConnection.ExecuteAsync($"DELETE FROM { TableName } WHERE Id LIKE @Id", new { Id = id }).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
