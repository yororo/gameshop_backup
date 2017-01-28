using System;
using System.Collections.Generic;

namespace GameShop.Data.EF.Entities
{
    internal class EfMeetupInformation : EfEntity
    {
        public Guid Id { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<EfMeetupLocation> MeetupLocations { get; set; } = new List<EfMeetupLocation>();
        public string Notes { get; set; }

        public Guid AdvertisementId { get; set; }
        public EfAdvertisement Advertisement { get; set; }
    }
}