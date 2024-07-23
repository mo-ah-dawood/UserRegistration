using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserRegistration.Core.Entities;

namespace UserRegistration.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<Privacy> Privacies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Verification>().HasKey(x => new { x.UserId, x.Type });
            base.OnModelCreating(builder);
        }
    }
}