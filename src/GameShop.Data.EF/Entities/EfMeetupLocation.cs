using System;

namespace GameShop.Data.EF.Entities
{
    internal class EfMeetupLocation : EfAddress
    {
        public Guid Id { get; set; }
        public string MeetupNotes { get; set; }
        
        public Guid MeetupInformationId { get; set; }
        public EfMeetupInformation MeetupInformation { get; set; }
    }
}