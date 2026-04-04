using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VagaBond.Backend.Models;

namespace VagaBond.Backend.Data
{
    public class VagaBondBackendContext : DbContext
    {
        public VagaBondBackendContext (DbContextOptions<VagaBondBackendContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Destination>(entity =>
            {
                entity.HasKey(e => e.DestinationId);
                entity.Property(e => e.CityName).IsRequired();
                entity.Property(e => e.Country).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.Rating).HasDefaultValue(3);
            });
        }

        public DbSet<Destination> Destination { get; set; } = default!;
    }
}
