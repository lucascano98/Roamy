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
            // EF Core doesn't automatically load related data (Days, Location) when fetching a Trip.
            // Include() tells EF Core to JOIN and load those related tables in the same query so the returned Trip object has its Days and Location lists populated.
            var trip = await _context.Trips.Include(t => t.Days).Include(t => t.Location).FirstOrDefaultAsync(t => t.TripId == id);
            return trip;
        }

        public async Task UpdateTripAsync(Trip trip)
        {
            _context.Trips.Update(trip);
            await SaveAsync();
        }
    }
}
