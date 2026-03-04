namespace Roamy.Models
{
    public class ActivityLocation
    {
        public TripLocation Area { get; set; } //gets the TripLocation
        public string? Address { get; set; }

        public ActivityLocation(TripLocation area, string? address)
        {
            Area = area;
            Address = address;
        }
    }
}
