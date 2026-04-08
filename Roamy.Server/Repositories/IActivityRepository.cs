using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity?> GetActivityByIdAsync(Guid id);
        Task<IEnumerable<Activity>> GetAllActivitiesByDayAsync(Guid dayId);
        Task<Activity?> AddActivityAsync(Activity activity);
        Task UpdateActivityAsync(Activity activity);
        Task DeleteActivityAsync(Guid id);
    }
}
