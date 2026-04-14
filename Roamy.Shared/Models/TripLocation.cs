namespace Roamy.Shared.Models
{
    public class TripLocation
    {
        public Guid TripLocationId { get; set; } = Guid.NewGuid();
        public Guid TripId { get; set; }
        public Trip? Trip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public double? Latitude {get; set;}
        public double? Longitude {get; set;}

        public TripLocation() { }

        //Constructor to create new TripLocation
        public TripLocation(string city, string country)
        {
            City = city;
            Country = country;
        }
    }
}
