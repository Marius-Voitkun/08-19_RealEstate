using _08_19_RealEstate.Models;
using Microsoft.EntityFrameworkCore;

namespace _08_19_RealEstate.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .Property(a => a.AreaInSqm)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Company)
                .WithMany(c => c.Apartments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Broker)
                .WithMany(b => b.Apartments)
                .OnDelete(DeleteBehavior.Restrict);


            // Configuring many-to-many relationship
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Brokers)
                .WithMany(b => b.Companies)
                .UsingEntity<CompanyBrokerJunction>(
                    j => j
                        .HasOne(cb => cb.Broker)    // Not Company - the order matters!!!
                        .WithMany()
                        .HasForeignKey(cb => cb.BrokerId)
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne(cb => cb.Company)
                        .WithMany()
                        .HasForeignKey(cb => cb.CompanyId)
                        .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
