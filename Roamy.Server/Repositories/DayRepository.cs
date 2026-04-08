using Microsoft.EntityFrameworkCore;
using Roamy.Server.Data;
using Roamy.Shared.Models;

namespace Roamy.Server.Repositories
{
    public class DayRepository : BaseRepository, IDayRepository
    {
        public DayRepository(AppDbContext context) : base(context) { }

        public async Task<Day?> AddDayAsync(Day day)
        {
            await _context.Days.AddAsync(day);
            await SaveAsync();
            return day;
        }

        public async Task DeleteDayAsync(Guid id)
        {
            var day = await GetDayByIdAsync(id);
            if (day == null)
                return;
            _context.Days.Remove(day);
            await SaveAsync();
        }

        public async Task<IEnumerable<Day>> GetAllDaysByTripAsync(Guid tripId)
        {
            var days = await _context.Days.Where(x => x.TripId == tripId).ToListAsync();
            return days;
        }

        public async Task<Day?> GetDayByIdAsync(Guid id)
        {
            var day = await _context.Days.FindAsync(id);
            return day;
        }

        public async Task UpdateDayAsync(Day day)
        {
            _context.Days.Update(day);
            await SaveAsync();
        }
    }
}
