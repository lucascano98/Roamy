using Microsoft.EntityFrameworkCore;
using Roamy.Server.Data;
using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public class TripRepository : BaseRepository, ITripRepository
    {
        public TripRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Trip?> AddTripAsync(Trip trip)
        {
            await _context.Trips.AddAsync(trip);
            await SaveAsync();
            return trip;
        }

        public async Task DeleteTripAsync(Guid id)
        {
            var trip = await GetTripByIdAsync(id);
            if (trip == null)
                return;
            _context.Trips.Remove(trip);
            await SaveAsync();
        }

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            var trips = await _context.Trips.ToListAsync();
            return trips;
        }

        public async Task<Trip?> GetTripByIdAsync(Guid id)
        { 
            var trip = await _context.Trips.FindAsync(id);
            return trip;
        }

        public async Task UpdateTripAsync(Trip trip)
        {
            _context.Trips.Update(trip);
            await SaveAsync();
        }
    }
}
