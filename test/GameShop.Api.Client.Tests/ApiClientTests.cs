using System;
using System.Net.Http;
using System.Threading.Tasks;
using GameShop.Contracts.Serialization.Json;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using Xunit;
using System.Collections.Generic;
using GameShop.Contracts.Entities.Products;

namespace GameShop.Api.Client.Tests
{
    public class ApiClientTests
    {
        private static ApiClient _apiClient = new ApiClient("http://localhost:6001");

        [Fact]
        public async Task AddAdvertisementWithGames()
        {
            Advertisement ad = new Advertisement();
            ad.Id = Guid.NewGuid();
            ad.FriendlyId = DateTime.Now.GetHashCode().ToString();
            ad.Description = "Awesome games for sell and trade!";

            for(int x = 0; x < 100; x++)
            {
                Game game = new Game();
                game.Id = Guid.NewGuid();
                game.Name = $"Game {x}";
                game.Description = $"Test Description {x}";
                game.GamingPlatform = GamingPlatform.PC;
                game.Genre = GameGenre.Action;
                game.ProductType = x % 2 == 0 ? ProductType.ForSale : ProductType.ForTrade;

                ad.Products.Add(game);
            }
            
            await _apiClient.Advertisements.AddAdvertisementAsync(ad);

            foreach(Product product in ad.Products)
            {
                Game game = await _apiClient.Products.Games.GetGameByIdAsync(product.Id);

                Assert.Equal(product.ProductType, game.ProductType);
                Assert.Equal(((Game)product).GamingPlatform, game.GamingPlatform);
                Assert.Equal(((Game)product).Genre, game.Genre);
            }
        }

        [Fact]
        public async Task AddAdvertisementWithGameConsoles()
        {
            Advertisement ad = new Advertisement();
            ad.Id = Guid.NewGuid();
            ad.FriendlyId = DateTime.Now.GetHashCode().ToString();
            ad.Description = "Awesome game consoles for sell and trade!";

            for(int x = 0; x < 100; x++)
            {
                GameConsole gameConsole = new GameConsole();
                gameConsole.Id = Guid.NewGuid();

                gameConsole.Name = $"Game Console {x}";
                gameConsole.Description = $"Description {x}";
                gameConsole.ConsolePlatform = $"PS{x}";
                gameConsole.ProductType = x % 2 == 0 ? ProductType.ForSale : ProductType.ForTrade;

                ad.Products.Add(gameConsole);
            }

            await _apiClient.Advertisements.AddAdvertisementAsync(ad);

            foreach(Product product in ad.Products)
            {
                GameConsole gameConsole = await _apiClient.Products.GameConsoles.GetGameConsoleByIdAsync(product.Id);

                Assert.Equal(product.ProductType, gameConsole.ProductType);
                Assert.Equal(((GameConsole)product).ConsolePlatform, gameConsole.ConsolePlatform);
            }
        }
    }
}