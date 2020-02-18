using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace EFRepository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Reseller> Resellers { get; set; }
    }
}