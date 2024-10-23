using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Data
{
    public class NotamManagementContext : DbContext
    {
        public NotamManagementContext(DbContextOptions<NotamManagementContext> options) : base(options) { }

        public DbSet<Notam> Notams { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<FlightPlan> FlightPlans { get; set; }
        public DbSet<NotamAction> NotamActions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
