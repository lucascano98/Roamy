using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public interface IDayRepository
    {
        Task<Day?> GetDayByIdAsync(Guid id);
        Task<IEnumerable<Day>> GetAllDaysByTripAsync(Guid tripId);
        Task<Day?> AddDayAsync(Day day);
        Task UpdateDayAsync(Day day);
        Task DeleteDayAsync(Guid id);
    }
}
