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

        public async Task UpdateActivityAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await SaveAsync();
        }
    }
}
