namespace Roamy.Shared.Models
{
    public class ActivityLocation
    {
        public Guid ActivityLocationId { get; set; } = Guid.NewGuid();
        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }
        public Guid AreaTripLocationId { get; set; } // explicit FK — EF uses this instead of shadow property
        public TripLocation? Area { get; set; } // nullable so EF won't traverse/re-insert the entity
        public string? Address { get; set; }

        public ActivityLocation() { }

        public ActivityLocation(TripLocation area, string? address)
        {
            AreaTripLocationId = area.TripLocationId; // set FK explicitly
            Address = address;
        }
    }
}
