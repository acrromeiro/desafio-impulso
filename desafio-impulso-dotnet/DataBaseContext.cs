using desafio_impulso_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_impulso_dotnet
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<School> School { get; set; }
        public DbSet<SchoolClass> SchoolClass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();
                entity.HasMany(c => c.SchoolClasses)
                    .WithOne(e => e.School);
            });

            modelBuilder.Entity<SchoolClass>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Grade)
                    .IsRequired();
                
                entity.HasOne(c => c.School)
                    .WithMany(e => e.SchoolClasses);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}