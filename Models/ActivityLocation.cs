namespace Roamy.Models
{
    public class ActivityLocation
    {
        public TripLocation Area { get; set; } //gets the TripLocation
        public string? Name { get; set; }
        public string? Address { get; set; }

        public ActivityLocation(TripLocation area, string? name, string? address)
        {
            Area = area;
            Name = name;
            Address = address;
        }
    }
}
