using LMS_V2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Data
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Email)
                .IsUnique();
        }

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }
}
