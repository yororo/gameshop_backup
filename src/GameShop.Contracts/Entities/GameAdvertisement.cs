using System.Collections.Generic;

namespace GameShop.Contracts.Entities
{
    public class GameAdvertisement : Advertisement<Game>
    {
        public GameAdvertisement()
        {
            
        }
        
        public GameAdvertisement(IEnumerable<Game> products) : base(products)
        {
        }
    }
}