using Roamy.Shared.Models;
using Roamy.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Roamy.Server.Repositories
{
    public class ActivityLocationRepository : BaseRepository, IActivityLocationRepository
    {
        public ActivityLocationRepository(AppDbContext context) : base(context) { }

        public async Task<ActivityLocation?> AddActivityLocationAsync(ActivityLocation activityLocation)
        {
            await _context.ActivityLocations.AddAsync(activityLocation);
            await SaveAsync();
            return activityLocation;
        }

        public async Task DeleteActivityLocationAsync(Guid id)
        {
            var activityLocation = await GetActivityLocationByIdAsync(id);
            if (activityLocation == null)
                return;
            _context.ActivityLocations.Remove(activityLocation);
            await SaveAsync();
        }

        public async Task<ActivityLocation?> GetActivityLocationByActivityAsync(Guid activityId)
        {
            var activityLocation = await _context.ActivityLocations.FirstOrDefaultAsync(x => x.ActivityId == activityId);
            return activityLocation;
        }

        public async Task<ActivityLocation?> GetActivityLocationByIdAsync(Guid id)
        {
            var activity = await _context.ActivityLocations.FindAsync(id);
            return activity;
        }

        public async Task UpdateActivityLocationAsync(ActivityLocation activityLocation)
        {
            _context.ActivityLocations.Update(activityLocation);
            await SaveAsync();
        }
    }
}
