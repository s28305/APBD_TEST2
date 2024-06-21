using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Helpers

{
    public class DriverContext : DbContext
    {
        public DriverContext()
        {
        }

        public DriverContext(DbContextOptions<DriverContext> options) : base(options)
        {
        }

        public virtual required DbSet<Car> Cars { get; set; }
        public virtual required DbSet<Driver> Drivers { get; set; }
        public virtual required DbSet<CarManufacturer> Manufacturers { get; set; }
        public virtual required DbSet<Competition> Competitions { get; set; }
        public virtual required DbSet<DriverCompetition> DriversCompetitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Car");

                entity.Property(e => e.ModelName)
                    .HasMaxLength(200)
                    .IsRequired();
                
                entity.Property(e => e.Number)
                    .IsRequired();

                entity.HasOne(c => c.CarManufacturer)
                    .WithMany()
                    .HasForeignKey(m => m.CarManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.ConcurrencyToken)
                    .IsConcurrencyToken();
            });
            
            modelBuilder.Entity<CarManufacturer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("CarManufacturer");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Competition");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Driver");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsRequired();
                
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsRequired();
                
                entity.Property(e => e.Birthday)
                    .IsRequired();

                entity.HasOne(c => c.Car)
                    .WithMany()
                    .HasForeignKey(c1 => c1.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.ConcurrencyToken)
                    .IsConcurrencyToken();
            });
            
            modelBuilder.Entity<DriverCompetition>(entity =>
            {
                entity.HasKey(e => e.DriverId);
                entity.HasKey(e => e.CompetitionId);

                entity.ToTable("DriverCompetition");

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.HasOne(c => c.Driver)
                    .WithMany()
                    .HasForeignKey(c1 => c1.DriverId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Competition)
                    .WithMany()
                    .HasForeignKey(c1 => c1.CompetitionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.ConcurrencyToken)
                    .IsConcurrencyToken();
            });

        }
    }
}