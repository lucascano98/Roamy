namespace Roamy.Models
{
    public class TripLocation
    {
        public string Country { get; set; }
        public string? State { get; set; }
        public string City { get; set; }

        //Constructor to create new TripLocation
        public TripLocation(string country, string? state, string city)
        {
            Country = country;
            State = state;
            City = city;
        }
    }
}
