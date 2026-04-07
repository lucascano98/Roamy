namespace Roamy.Shared.Models
{
    public class TripLocation
    {
        public Guid TripLocationId { get; set; } = Guid.NewGuid();

        public string Country { get; set; }
        public string City { get; set; }

        public TripLocation() { }

        //Constructor to create new TripLocation
        public TripLocation(string city, string country)
        {
            City = city;
            Country = country;
        }
    }
}
