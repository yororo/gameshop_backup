using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Interface.Entities;
using System.Data;
using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;
using GameShop.Interface.Enumerations;

namespace GameShop.Data.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        private List<PCGame> _pcGames;
        private List<ConsoleGame> _consoleGames;

        public ProductRepository(IDatabaseConnectionProvider provider) : base(provider)
        {
            _pcGames = new List<PCGame>()
            {
                new PCGame() { Id = Guid.NewGuid(), Name = "Test 1", Genre = GameGenre.Action },
                new PCGame() { Id = Guid.NewGuid(), Name = "Test 2", Genre = GameGenre.MMORPG },
                new PCGame() { Id = Guid.NewGuid(), Name = "Test 3", Genre = GameGenre.RPG },
                new PCGame() { Id = Guid.NewGuid(), Name = "Test 4", Genre = GameGenre.Strategy },
                new PCGame() { Id = Guid.NewGuid(), Name = "Name 5", Genre = GameGenre.Simulation, MinimumRequirements = "Test Minimum Requirements" }
            };

            _consoleGames = new List<ConsoleGame>()
            {
                new PlayStation2Game() { Id = Guid.NewGuid(), Name = "PS2 Game", Genre = GameGenre.RPG },
                new XboxOneGame() { Id = Guid.NewGuid(), Name = "Wii Game", Genre = GameGenre.Simulation }
            };
        }

        public Product GetProductById(Guid id)
        {
            return _pcGames.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> FindProductsByName(string name)
        {
            return _pcGames.FindAll(p => p.Name.Contains(name));
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();
            products.AddRange(_pcGames);
            products.AddRange(_consoleGames);

            return products;
        }

        public bool DeleteProduct(Guid id)
        {
            return _pcGames.Remove(_pcGames.FirstOrDefault(p => p.Id == id));
        }

        /// <summary>
        /// Map IDataRecord from DataReader to domain object.
        /// </summary>
        /// <param name="record">IDataRecord from DataReader. A single data records represents a single row in the database.</param>
        /// <returns>Mapped domain object.</returns>
        protected override object MapResult(IDataRecord record)
        {
            var product = new PCGame();
            product.Id = (Guid)record["Id"];
            product.Name = record["Title"].ToString();

            return product;
        }
    }
}
