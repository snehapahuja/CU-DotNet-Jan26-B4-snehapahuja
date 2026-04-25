using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseAPI.Models;

namespace CourseAPI.Data
{
    public class CourseAPIContext : DbContext
    {
        public CourseAPIContext (DbContextOptions<CourseAPIContext> options)
            : base(options)
        {
        }

        public DbSet<CourseAPI.Models.Course> Courses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title).IsRequired().HasMaxLength(50);

                entity.Property(x => x.Summary).IsRequired().HasMaxLength(1000);

                entity.Property(x => x.Price).IsRequired();
            });
        }
    }
}
