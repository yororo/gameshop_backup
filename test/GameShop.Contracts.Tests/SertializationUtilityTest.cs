using System;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;
using GameShop.Contracts.Serialization.Json;
using Newtonsoft.Json;
using Xunit;

namespace test.GameShop.Contracts.Tests
{
    public class SertializationUtilityTest
    {
        [Fact]
        public void GameProductSerialization()
        {
            Game game = new Game();
            game.Id = Guid.NewGuid();
            game.GamingPlatform = GamingPlatform.GameBoy;
            game.Genre = GameGenre.Action;

            string json = SerializationUtility.SerializeToJson(game);

            Product product = SerializationUtility.DeserializeProductJson(json);

            Game g = (Game)product;

            Assert.Equal(game.GamingPlatform, g.GamingPlatform);
            Assert.Equal(game.Genre, g.Genre);
        }

        [Fact]
        public void GameConsoleProductSerialization()
        {
            GameConsole gameConsole = new GameConsole();
            gameConsole.Id = Guid.NewGuid();
            gameConsole.ConsolePlatform = "PS4";

            string json = SerializationUtility.SerializeToJson(gameConsole);

            Product product = SerializationUtility.DeserializeProductJson(json);

            GameConsole g = (GameConsole)product;

            Assert.Equal(gameConsole.ConsolePlatform, g.ConsolePlatform);
        }
    }
}