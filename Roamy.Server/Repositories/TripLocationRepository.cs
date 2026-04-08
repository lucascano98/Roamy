using Roamy.Shared.Models;
using Roamy.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Roamy.Server.Repositories
{
    public class TripLocationRepository : BaseRepository, ITripLocationRepository
    {
        public TripLocationRepository(AppDbContext context) : base(context) { }

        public async Task<TripLocation?> AddTripLocationAsync(TripLocation tripLocation)
        {
            await _context.TripLocations.AddAsync(tripLocation);
            await SaveAsync();
            return tripLocation;
        }

        public async Task DeleteTripLocationAsync(Guid id)
        {
            var tripLocation = await GetTripLocationByIdAsync(id);
            if (tripLocation == null)
                return;
            _context.TripLocations.Remove(tripLocation);
            await SaveAsync();
        }

        public async Task<IEnumerable<TripLocation>> GetAllTripLocationsByTripAsync(Guid tripId)
        {
            var tripLocations = await _context.TripLocations.Where(x => x.TripId == tripId).ToListAsync();
            return tripLocations;
        }

        public async Task<TripLocation?> GetTripLocationByIdAsync(Guid id)
        {
            var tripLocation = await _context.TripLocations.FindAsync(id);
            return tripLocation;
        }

        public async Task UpdateTripLocationAsync(TripLocation tripLocation)
        {
            _context.TripLocations.Update(tripLocation);
            await SaveAsync();
        }
    }
}
