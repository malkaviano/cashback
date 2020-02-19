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
                .Property(s => s.Id)
                .HasColumnType("bigint");

            modelBuilder.Entity<Sales>()
                .Property(s => s.Data)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(s => s.Code)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(s => s.Value)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(s => s.Value)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Sales>()
                .Property(s => s.CashbackValue)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Sales>()
                .Property(s => s.Cpf)
                .IsRequired();

            modelBuilder.Entity<Reseller>()
                .Property(s => s.Id)
                .HasColumnType("bigint");

            modelBuilder.Entity<Reseller>()
                .Property(r => r.Cpf)
                .IsRequired();

            modelBuilder.Entity<Reseller>()
                .HasIndex(r => r.Cpf)
                .IsUnique();

            modelBuilder.Entity<Reseller>()
                .Property(r => r.Email)
                .IsRequired();

            modelBuilder.Entity<Reseller>()
                .HasIndex(r => r.Email)
                .IsUnique();

            modelBuilder.Entity<Reseller>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Reseller>()
                .HasMany(s => s.Sales)
                .WithOne(r => r.Reseller);
        }
    }
}