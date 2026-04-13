using Roamy.Shared.Models;
using Roamy.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Roamy.Server.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context) { }

        public async Task<Activity?> AddActivityAsync(Activity activity)
        {
            // Clear the Area navigation property so EF won't try to re-insert the existing
            // TripLocation. AreaTripLocationId (the explicit FK) is already set and is enough
            // for EF to write the correct foreign key value when inserting the ActivityLocation row.
            if (activity.Location is not null)
                activity.Location.Area = null;

            await _context.Activities.AddAsync(activity);
            await SaveAsync();
            return activity;
        }

        public async Task DeleteActivityAsync(Guid id)
        {
            var activity = await GetActivityByIdAsync(id);
            if (activity == null)
                return;
            _context.Activities.Remove(activity);
            await SaveAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            return activity;
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByDayAsync(Guid dayId)
        {
            var activities = await _context.Activities.Where(x => x.DayId == dayId).ToListAsync();
            return activities;
        }

        public async Task<IEnumerable<Activity>> GetShortlistByTripAsync(Guid tripId)
        {
            //Join Activities to Days on DayId, filter where the day belongs to the trip AND the activity has no date, then return just the activities.
            var activities = await _context.Activities
                .Join(_context.Days, activity => activity.DayId, day => day.DayId, (activity, day) => new { activity, day })
                .Where(x => x.day.TripId == tripId && x.activity.Date == null)
                .Select(x => x.activity).ToListAsync();
            return activities;
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            var existing = await _context.Activities
                .Include(a => a.Location)
                .FirstAsync(a => a.ActivityId == activity.ActivityId);

            existing.Name = activity.Name;
            existing.Category = activity.Category;
            existing.Date = activity.Date;
            existing.DayId = activity.DayId;
            existing.StartTime = activity.StartTime;
            existing.EndTime = activity.EndTime;
            existing.Notes = activity.Notes;

            if (activity.Location != null)
            {
                if (existing.Location != null)
                {
                    existing.Location.AreaTripLocationId = activity.Location.AreaTripLocationId;
                    existing.Location.Address = activity.Location.Address;
                }
                else
                {
                    existing.Location = new ActivityLocation
                    {
                        AreaTripLocationId = activity.Location.AreaTripLocationId,
                        Address = activity.Location.Address
                    };
                }
            }

            await SaveAsync();
        }
    }
}
