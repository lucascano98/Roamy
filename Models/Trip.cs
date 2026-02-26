using System.Diagnostics;

namespace Roamy.Models
{
    public class Trip
    {
        public Guid TripId { get; private set; } = Guid.NewGuid(); //Guid = Globally Unique Identifier
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TripLocation> Location { get; set; } = new List<TripLocation>(); //creates an empty list once class is created
        public List<Day> Days { get; set; } = new List<Day>();

        public Trip(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            CreateDays();
        }

        public Day? GetDayByDate(DateTime activityDate)
        {
            int dayIndex = (activityDate.Date - StartDate.Date).Days;
            if (dayIndex < 0 || dayIndex >= Days.Count)
            {
                //Print: date is outside of trip range.
                return null;
            }

            return Days[dayIndex];
        }

        public void AddActivityByDate(DateTime activityDate, string name, TimeSpan? startTime, TimeSpan? endTime, string? notes = null)
        {
            Day? targetDay = GetDayByDate(activityDate);
            if (targetDay == null)
                //exception?
                return;

            Activity newActivity = new Activity(name, targetDay.DayId, activityDate, startTime, endTime, notes);
            targetDay.AddActivity(newActivity);
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


        //Fetch Day by num("Day 3")
        //Add Day(s)
        //Delete Day(s)
        //Move activities across days
        //Search for activities within the whole trip
        //Add location(s)
        //Remove location(s)
        //(Optional) View all activities across all days (table view)
    }
}
