using System;

namespace GameShop.Data.EF.Entities
{
    internal class EfAdvertisementProducts
    {
        public Guid AdvertisementId { get; set; }
        public EfAdvertisement Advertisement { get; set; }

        public Guid ProductId { get; set; }
        public EfProduct Product { get; set; }
    }
}