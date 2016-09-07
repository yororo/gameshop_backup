using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Game : Product
    {
        public Game()
        {
            Platform = GamingPlatform.None;
            Genre = GameGenre.None;
            ReleaseDate = DateTime.MaxValue;
        }

        public GamingPlatform Platform { get; set; }
        public GameGenre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
