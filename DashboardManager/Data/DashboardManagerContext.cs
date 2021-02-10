using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Models;

namespace DashboardManager.Data
{
    public class DashboardManagerContext : DbContext
    {
        public DashboardManagerContext (DbContextOptions<DashboardManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Commercial> Commercial { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Departement> Departement { get; set; }
    }
}
