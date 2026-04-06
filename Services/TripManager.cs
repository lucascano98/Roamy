using Roamy.Shared.Models;
using System.Runtime.CompilerServices;

namespace Roamy.Services
{
    public class TripManager
    {
        public Trip CurrentTrip { get; set; }

        public void StartTrip(TripLocation location, DateTime startDate, DateTime endDate)
        {
            CurrentTrip = new Trip(location, startDate, endDate);
             
        }
        public TripManager()
        {

        }

        public event Action? OnChange; //Action is a delegate

        public void AddActivity(Activity activity)
        {
            if (activity.Date == null || activity.StartTime == null)
                AddToShortlist(activity);
            else
                CurrentTrip.AddActivityByDate(activity);

            OnChange?.Invoke(); //If anything is subscribed to OnChange, call it. Else, do nothing
            SaveAsync();
        }

        public void EditActivity(Activity activity)
        {
            var currentDay = CurrentTrip.Days.FirstOrDefault(d => d.Activities.Contains(activity));
            if (currentDay == null) return;

            currentDay.Activities.Remove(activity);
            CurrentTrip.AddActivityByDate(activity);

            OnChange?.Invoke();
            SaveAsync();
        }

        public void DeleteActivity(Activity activity)
        {
            var currentDay = CurrentTrip.Days.FirstOrDefault(d => d.Activities.Contains(activity));
            if (currentDay == null) return;

            currentDay.Activities.Remove(activity);

            OnChange?.Invoke();
            SaveAsync();
        }

        public void AddToShortlist(Activity activity)
        {
            CurrentTrip.Shortlist.Add(activity);

            OnChange?.Invoke();
            SaveAsync();
        }

        public void RemoveFromShortlist(Activity activity)
        {
            CurrentTrip.Shortlist.Remove(activity);

            OnChange?.Invoke();
            SaveAsync();
        }

        public void EditShortListActivity(Activity original, Activity updated)
        {
            original.Name = updated.Name;
            original.Category = updated.Category;
            original.Location = updated.Location;
            original.Date = updated.Date;
            original.StartTime = updated.StartTime;
            original.EndTime = updated.EndTime;
            original.Notes = updated.Notes;

            OnChange?.Invoke();
            SaveAsync();
        }

        //Future implementation to save Trip data
        private void SaveAsync()
        {

        }
    }
}
