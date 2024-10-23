using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotamManagement.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NotamManagement.Core.Data
{
    public class NotamManagementContext : IdentityDbContext<User>
    {

        public NotamManagementContext(DbContextOptions<NotamManagementContext> options) : base(options) { }

        public DbSet<Notam> Notams { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<FlightPlan> FlightPlans { get; set; }
        public DbSet<NotamAction> NotamActions { get; set; }
        public DbSet<Organization> Organizations { get; set; }




    }
}
