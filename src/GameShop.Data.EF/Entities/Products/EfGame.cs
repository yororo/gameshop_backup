using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities.Products
{
    internal class EfGame : EfProduct
    {
        //public string Title { get; set; }
        public GameGenre Genre { get; set; }
        public GamingPlatform GamingPlatform { get; set; }
        // public virtual EfGameSellingInformation SellingInformation { get; set; }
        // public virtual EfGameTradingInformation TradingInformation { get; set; }

        // public Guid? AdvertisementId { get; set; }
        // public virtual EfGameAdvertisement Advertisement { get; set; }
    }
}
