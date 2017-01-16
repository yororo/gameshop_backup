using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Game : Product
    {
        #region Properties

        public GamingPlatform GamingPlatform { get; set; }

        public GameGenre Genre { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Game()
        {
            GamingPlatform = GamingPlatform.Unspecified;
            Genre = GameGenre.Unspecified;
        }

        #endregion Constructors
    }
}