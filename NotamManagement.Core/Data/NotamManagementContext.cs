using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Data
{
    public class NotamManagementContext : DbContext
    {
        public DbSet<Notam> Notams { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<FlightPlan> FlightPlans { get; set; }
        public DbSet<NotamAction> NotamActions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host=Hjem.jazper.dk;Database=notam_management;Username=master;Password=9ycLQnsHBy34gtI");
        }




    }
}
