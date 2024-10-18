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
        public DbSet<Shared.Models.Notam> Notams { get; set; }




    }
}
