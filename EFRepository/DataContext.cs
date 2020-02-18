using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFRepository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Reseller> Resellers { get; set; }

        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>()
                .Property(r => r.Value)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Reseller>()
                .HasMany(s => s.Sales)
                .WithOne(r => r.Reseller);
        }
    }
}