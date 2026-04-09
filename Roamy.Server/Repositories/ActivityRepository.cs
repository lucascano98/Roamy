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
            _context.Activities.Update(activity);
            await SaveAsync();
        }
    }
}
