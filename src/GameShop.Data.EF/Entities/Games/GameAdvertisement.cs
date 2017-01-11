using System.Collections.Generic;

namespace GameShop.Data.EF.Entities.Games
{
    internal class GameAdvertisement : Advertisement
    {
        public virtual ICollection<Game> Games { get; set; }
    }
}