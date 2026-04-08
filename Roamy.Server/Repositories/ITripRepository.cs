using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public interface ITripRepository
    {
        Task<Trip?> GetTripByIdAsync(Guid id);
        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip?> AddTripAsync(Trip trip);
        Task UpdateTripAsync(Trip trip);
        Task DeleteTripAsync(Guid id);
    }
}
