namespace GameShop.Contracts.Entities
{
    public class MeetupLocation : Address
    {
        public string MeetupNotes { get; set; }

        // Future feature: Ability to display meetup locations in Google Maps.
        //public object GeoCoordinates { get; set; }
    }
}