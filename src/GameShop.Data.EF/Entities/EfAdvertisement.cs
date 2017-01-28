using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Enumerations;

namespace GameShop.Data.EF.Entities
{
    internal class EfAdvertisement : EfEntity
    {
        public Guid Id { get; set; }
        public string FriendlyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AdvertisementState State { get; set; }

        public EfMeetupInformation MeetupInformation { get; set; }

        public ICollection<EfAdvertisementProducts> AdvertisementProducts { get; set; } = new List<EfAdvertisementProducts>();
    }
}
