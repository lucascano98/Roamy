namespace Roamy.Shared.Models
{
    public class ActivityLocation
    {
        public Guid ActivityLocationId { get; set; } = Guid.NewGuid();
        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }
        public TripLocation Area { get; set; } //gets the TripLocation
        public string? Address { get; set; }

        public ActivityLocation() { }

        public ActivityLocation(TripLocation area, string? address)
        {
            Area = area;
            Address = address;
        }
    }
}
