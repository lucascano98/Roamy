using Roamy.Shared.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Roamy.Services
{
    public class TripManager
    {
        public Trip? CurrentTrip { get; set; }
        private readonly HttpClient _http;

        public async Task<Trip> StartTrip(TripLocation location, DateTime startDate, DateTime endDate)
        {
            var trip = new Trip(location, startDate, endDate); //construct a new trip
            var response = await _http.PostAsJsonAsync("api/trips", trip); //POST the new trip
            var createdTrip = await response.Content.ReadFromJsonAsync<Trip>(); //confirm the trip has been created
            if (createdTrip == null)
                throw new NullReferenceException("Failed to create a trip.");
            CurrentTrip = createdTrip; //set the new created trip to CurrentTrip
            return createdTrip; //return the created trip
        }
        public TripManager(HttpClient http)
        {
            _http = http;
        }

        public event Action? OnChange; //Action is a delegate

        public async Task<Trip> GetTripAsync(Guid tripId)
        {
            var trip = await _http.GetFromJsonAsync<Trip>($"api/trips/{tripId}");
            if (trip == null)
                throw new NullReferenceException("Failed to find a trip.");
            CurrentTrip = trip;
            return CurrentTrip;
        }

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
