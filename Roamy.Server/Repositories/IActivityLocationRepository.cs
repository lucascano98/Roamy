using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public interface IActivityLocationRepository
    {
        Task<ActivityLocation?> GetActivityLocationByIdAsync(Guid id);
        Task<ActivityLocation?> GetActivityLocationByActivityAsync(Guid activityId);
        Task<ActivityLocation?> AddActivityLocationAsync(ActivityLocation activityLocation);
        Task UpdateActivityLocationAsync(ActivityLocation activityLocation);
        Task DeleteActivityLocationAsync(Guid id);
    }
}