using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class Advertisement : Entity
    {
        public Guid AdvertisementId { get; set; }
        public string FriendlyId { get; set; }
        public AdvertisementState State { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
