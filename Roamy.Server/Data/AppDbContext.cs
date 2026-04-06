using Roamy.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Roamy.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<TripLocation> TripLocations { get; set; }
        public DbSet<ActivityLocation> ActivityLocations { get; set; }
    }
}
