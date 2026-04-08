using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public interface ITripLocationRepository
    {
        Task<TripLocation?> GetTripLocationByIdAsync(Guid id);
        Task<IEnumerable<TripLocation>> GetAllTripLocationsByTripAsync(Guid tripId);
        Task<TripLocation?> AddTripLocationAsync(TripLocation tripLocation);
        Task UpdateTripLocationAsync(TripLocation tripLocation);
        Task DeleteTripLocationAsync(Guid id);
    }
}
