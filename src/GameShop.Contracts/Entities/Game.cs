using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Game : Product
    {
        #region Declarations
        
        private GamingPlatform _gamingPlatform;
        private GameGenre _gameGenre;

        #endregion Declarations

        #region Properties

        public GamingPlatform GamingPlatform
        {
            get
            {
                return _gamingPlatform;
            }

            set
            {
                _gamingPlatform = value;
            }
        }

        public GameGenre GameGenre
        {
            get
            {
                return _gameGenre;
            }

            set
            {
                _gameGenre = value;
            }
        }

        #endregion Properties

        #region Constructors

        public Game()
        {
            GamingPlatform = GamingPlatform.Unspecified;
            GameGenre = GameGenre.Unspecified;
        }

        #endregion Constructors
    }
}