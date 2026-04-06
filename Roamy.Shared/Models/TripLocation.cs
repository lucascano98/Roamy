namespace Roamy.Shared.Models
{
    public class TripLocation
    {
        public string Country { get; set; }
        public string City { get; set; }

        //Constructor to create new TripLocation
        public TripLocation(string city, string country)
        {
            City = city;
            Country = country;
        }
    }
}
