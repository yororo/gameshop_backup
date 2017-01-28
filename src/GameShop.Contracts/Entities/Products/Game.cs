using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities.Products
{
    public class Game : Product
    {
        #region Properties

        public GamingPlatform GamingPlatform { get; set; }

        public GameGenre Genre { get; set; }

        public override ProductCategory Category 
        { 
            get 
            { 
                return ProductCategory.Games; 
            } 
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Game()
            : base()
        {
            GamingPlatform = GamingPlatform.Unspecified;
            Genre = GameGenre.Unspecified;
        }

        #endregion Constructors
    }
}