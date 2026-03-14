using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinTrackPro.Models;

namespace FinTrackPro.Data
{
    public class FinTrackProContext : DbContext
    {
        public FinTrackProContext (DbContextOptions<FinTrackProContext> options)
            : base(options)
        {
        }

        public DbSet<FinTrackPro.Models.Account> Account { get; set; } = default!;
    }
}
