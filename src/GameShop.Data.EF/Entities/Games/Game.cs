using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities.Games
{
    internal class Game : Product
    {
        public string Title { get; set; }
        public GameGenre GameGenre { get; set; }
        public GamingPlatform GamePlatform { get; set; }
        public virtual GameSellingInformation SellingInformation { get; set; }
        public virtual GameTradingInformation TradingInformation { get; set; }

        public Guid AdvertisementId { get; set; }
        public virtual GameAdvertisement Advertisement { get; set; }
    }
}
