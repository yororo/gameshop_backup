using System.Collections.Generic;

namespace GameShop.Contracts.Entities
{
    public class GameAdvertisement : Advertisement<Game>
    {
        #region Constructors
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameAdvertisement()
            : base()
        {
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="games">Advertisement games.</param>
        public GameAdvertisement(IEnumerable<Game> games) 
            : base(games)
        {
        }

        #endregion Constructors
    }
}