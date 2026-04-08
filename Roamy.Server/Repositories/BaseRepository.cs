using Roamy.Server.Data;

namespace Roamy.Server.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        protected async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
