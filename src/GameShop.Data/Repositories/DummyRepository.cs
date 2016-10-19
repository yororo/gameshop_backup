using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Providers;

namespace GameShop.Data.Repositories
{
    public class DummyRepository : Repository, IGameAdvertisementRepository, IGameRepository
    {
        private List<Advertisement> _ads;
        private List<PCGame> _pcGames;
        private List<ConsoleGame> _consoleGames;

        public DummyRepository(IDatabaseProviderClient databaseProviderClient)
            : base(databaseProviderClient)
        {
            _ads = new List<Advertisement>();

            _pcGames = new List<PCGame>()
            {
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Test 1", GameGenre = GameGenre.Action },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Dragon Age 3: Inquisition", GameGenre = GameGenre.RPG },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Witcher 3", GameGenre = GameGenre.RPG },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Mass Effect 4", GameGenre = GameGenre.SciFi },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Torchlight", GameGenre = GameGenre.Simulation, SystemRequirements = new ComputerSpecification() { CPU = new CPU() { Cores = 4, Name = "Intel i7 5700", ClockSpeed = "3.40" } } }
            };


            _consoleGames = new List<ConsoleGame>()
            {
                new ConsoleGame() { ProductId = Guid.NewGuid(), Name = "PS2 Game", GameGenre = GameGenre.RPG, GamingPlatform = GamingPlatform.Xbox360 },
                new ConsoleGame() { ProductId = Guid.NewGuid(), Name = "3DS Game", GameGenre = GameGenre.Simulation, GamingPlatform = GamingPlatform.PlayStation2 }
            };


            var pcGamesAd = new Advertisement(_pcGames);
            pcGamesAd.AdvertisementId = Guid.NewGuid();
            pcGamesAd.FriendlyId = "123";
            pcGamesAd.Title = "PC Games For Sale!";
            pcGamesAd.Description = "Test Description For PC Games Ad.";

            var consoleGamesAd = new Advertisement(_consoleGames);
            consoleGamesAd.AdvertisementId = Guid.NewGuid();
            consoleGamesAd.FriendlyId = "321";
            consoleGamesAd.Title = "Console Games For Sale!";
            consoleGamesAd.Description = "Test Description For Console Games Ad.";

            _ads.Add(pcGamesAd);
            _ads.Add(consoleGamesAd);

        }

        public Task<int> AddAsync(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement> FindByFriendlyIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetOwnerAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> GetAllDeepAsync()
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

        public Task<IEnumerable<Address>> GetMeetupLocationsAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetProductsAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement<Game>>> GetByGameReleaseDateAsync(DateTime releaseDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement<Game>>> GetByGamingPlatformAsync(GamingPlatform gamingPlatform)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement<Game>>> GetByGameGenreAsync(GameGenre gameGenre)
        {
            throw new NotImplementedException();
        }

        Task<Advertisement<Game>> IAdvertisementRepository<Guid, Game>.FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Advertisement<Game>> IAdvertisementRepository<Guid, Game>.FindByFriendlyIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.GetAllDeepAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Game>> IProductRepository<Guid, Game>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
