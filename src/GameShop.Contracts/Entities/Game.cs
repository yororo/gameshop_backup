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

        private Guid _gameId;
        private GamingPlatform _gamingPlatform;
        private GameGenre _gameGenre;
        private DateTime _releaseDate;

        #endregion

        #region Properties

        public Guid GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }
        
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

        public DateTime ReleaseDate
        {
            get
            {
                return _releaseDate;
            }

            set
            {
                _releaseDate = value;
            }
        }

        #endregion

        #region Constructors

        public Game()
        {
            GameId = Guid.Empty;
            GamingPlatform = GamingPlatform.None;
            GameGenre = GameGenre.None;
            ReleaseDate = DateTime.MaxValue;
        }

        #endregion
    }
}