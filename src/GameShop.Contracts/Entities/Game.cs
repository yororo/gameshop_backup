using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Game : Product
    {
        #region Fields
        
        private GamingPlatform _gamingPlatform;
        private GameGenre _gameGenre;
        private string _title;

        #endregion

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

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        #endregion

        #region Constructors

        public Game()
        {
            GamingPlatform = GamingPlatform.None;
            GameGenre = GameGenre.None;
        }

        #endregion
    }
}