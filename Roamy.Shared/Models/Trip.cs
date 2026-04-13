using System.Diagnostics;

namespace Roamy.Shared.Models
{
    public class Trip
    {
        public Guid TripId { get; set; } = Guid.NewGuid(); //Guid = Globally Unique Identifier
        public string Name { get; set; } = "MyTrip";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TripLocation> Location { get; set; } = new List<TripLocation>(); //creates an empty list once class is created
        public List<Day> Days { get; set; } = new List<Day>();
        public List<Activity> Shortlist { get; set; } = new List<Activity>();

        public Trip() { }

        public Trip(TripLocation location, DateTime startDate, DateTime endDate)
        {
            Location.Add(location);
            StartDate = startDate;
            EndDate = endDate;
            CreateDays();
        }

        public Day? GetDayByDate(DateTime activityDate)
        {
            return Days.FirstOrDefault(d => d.Date.Date == activityDate.Date);
        }

        public void AddActivityByDate(Activity activity)
        {
            if (activity.Date == null) return;

            Day? targetDay = GetDayByDate(activity.Date.Value);
            if (targetDay == null)
                //exception?
                return;

            activity.DayId = targetDay.DayId;
            targetDay.AddActivity(activity);
        }

        public void CreateDays()
        {
            TimeSpan TripLength = EndDate - StartDate;
            int duration = TripLength.Days + 1;
            for (int i = 0; i < duration; i++)
            {
                Day newDay = new Day(TripId, StartDate.AddDays(i), i + 1);
                Days.Add(newDay);
            }
        }
    }
}
