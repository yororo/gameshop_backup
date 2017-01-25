using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities.Products
{
    internal class EfProduct : EfEntity
    {
        public Guid Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public ProductCategory Category { get; set; }

        public EfSellingInformation SellingInformation { get; set; }
        
        public EfTradingInformation TradingInformation { get; set; }

        public ICollection<EfAdvertisementProducts> AdvertisementProducts { get; set; } = new List<EfAdvertisementProducts>();
    }
}
