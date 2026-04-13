using Roamy.Shared.Models;
using System.Net.Http.Json;

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
            trip.Shortlist = trip.Days
                .SelectMany(d => d.Activities)
                .Where(a => a.Date == null)
                .ToList();
            CurrentTrip = trip;
            return CurrentTrip;
        }

        public async Task AddActivity(Activity activity)
        {
            if (CurrentTrip == null) throw new InvalidOperationException("No trip loaded.");
            var targetDay = activity.Date == null
                ? CurrentTrip.Days[0]
                : CurrentTrip.GetDayByDate(activity.Date.Value);
            var dayId = targetDay?.DayId
                ?? throw new InvalidOperationException($"No day found for date {activity.Date}.");

            var response = await _http.PostAsJsonAsync($"api/days/{dayId}/activities", activity);
            var created = await response.Content.ReadFromJsonAsync<Activity>()
                ?? throw new NullReferenceException("Failed to create activity.");

            targetDay.AddActivity(created);
            if (created.Date == null)
                CurrentTrip.Shortlist.Add(created);

            OnChange?.Invoke();
        }

        public async Task EditActivity(Activity activity)
        {
            if (CurrentTrip == null) throw new InvalidOperationException("No trip loaded.");

            // Reroute to the correct day based on the (possibly changed) date.
            // Date == null means it stays/returns to the shortlist, which lives in Days[0].
            var targetDay = activity.Date == null
                ? CurrentTrip.Days[0]
                : CurrentTrip.GetDayByDate(activity.Date.Value);
            activity.DayId = targetDay?.DayId
                ?? throw new InvalidOperationException($"No day found for date {activity.Date}.");

            var response = await _http.PutAsJsonAsync($"api/activities/{activity.ActivityId}", activity);
            response.EnsureSuccessStatusCode();
            await GetTripAsync(CurrentTrip.TripId);
            OnChange?.Invoke();
        }

        public async Task DeleteActivity(Activity activity)
        {
            if (CurrentTrip == null) throw new InvalidOperationException("No trip loaded.");
            await _http.DeleteAsync($"api/activities/{activity.ActivityId}");

            var ownerDay = CurrentTrip.Days.FirstOrDefault(d => d.DayId == activity.DayId);
            ownerDay?.DeleteActivity(activity.ActivityId);
            CurrentTrip.Shortlist.Remove(activity);

            OnChange?.Invoke();
        }
    }
}
