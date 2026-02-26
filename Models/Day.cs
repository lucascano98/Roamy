using System.Diagnostics;
using System.Xml.Linq;

namespace Roamy.Models
{
    public class Day
    {
        public Guid DayId { get; private set; } = Guid.NewGuid();
        public Guid TripId { get; }
        DateTime Date { get; }
        public int DayNumber { get; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public string? Weather { get; set; }
        //Time conflict validation

        public Day(Guid tripId, DateTime date, int dayNumber)
        {
            TripId = tripId;
            Date = date;
            DayNumber = dayNumber;
        }

        //Add Activity
        public void AddActivity(Activity activity)
        {
            if (activity.StartTime == null)
            {
                Activities.Add(activity);
                return;
            }

            int index = this.Activities.FindIndex(a => a.StartTime > activity.StartTime); //keeps list sorted by placing new acitivities in the correct index.
            if (index == -1)
                this.Activities.Add(activity);
            else
                Activities.Insert(index, activity);
        }

        //Delete Activity
        public void DeleteActivity(Guid activityId)
        {
            Activity? toRemove = this.Activities.FirstOrDefault(a => a.ActivityId == activityId);
            if (toRemove != null)
                this.Activities.Remove(toRemove);
        }

        public void RescheduleActivity(Activity activity)
        {
            Activities.Remove(activity);
            AddActivity(activity);
        }

        public bool WouldConflict(Activity newActivity)
        {
            return Activities.Any(a => newActivity.CheckOverlap(a));
        }
    }
}
