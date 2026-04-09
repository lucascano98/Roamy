namespace Roamy.Shared.Models
{
    public enum ActivityCategory
    {
        Sightseeing,
        FoodAndDrink,
        Transport,
        Lodging,
        Event,
        Other
    }

    public class Activity
    {
        public Guid ActivityId { get; set; } = Guid.NewGuid();
        public Guid? DayId { get; set; }
        public string Name { get; set; }
        public ActivityCategory? Category { get; set; }
        public ActivityLocation? Location { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Notes { get; set; }

        public TimeSpan? Duration => EndTime - StartTime;

        public Activity() { }

        public Activity(string name, ActivityCategory? category, Guid? dayId = null, DateTime? date = null, TimeSpan ? startTime = null, TimeSpan? endTime = null, string? notes = null)
        {
            DayId = dayId;
            Name = name;
            Category = category;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Notes = notes;
        }

        //Activity overlap detection
        public bool CheckOverlap(Activity other)
        {
            return this.StartTime < other.EndTime && other.StartTime < this.EndTime;
        }
    }
}
