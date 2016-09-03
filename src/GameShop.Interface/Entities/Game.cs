using GameShop.Interface.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Interface.Entities
{
    public abstract class Game : Product
    {
        public Game()
        {
            Genre = GameGenre.None;
            ReleaseDate = DateTime.MaxValue;
        }

        public GameGenre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
