using CvSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CvSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CV> CVs => Set<CV>();
        public DbSet<ExperienceInformation> Experiences => Set<ExperienceInformation>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CV>().OwnsOne((o) => o.PersonalInformation, (conf) =>
            {
                conf.WithOwner();
                conf.ToTable("PersonalInformation");
            });

            builder.Entity<CV>()
            .HasMany((o) => o.Experiences)
            .WithOne((o) => o.CV)
            .HasForeignKey((x) => x.CVId);
            base.OnModelCreating(builder);
        }
    }
}