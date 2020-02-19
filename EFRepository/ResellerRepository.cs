using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFRepository
{
    public class ResellerRepository : IResellerRepository
    {
        private readonly DataContext context;

        public ResellerRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Reseller> Get(string key)
        {
            return await context.Resellers.Where(r => r.Cpf == key).SingleOrDefaultAsync();
        }

        public async Task Create(Reseller entity)
        {
            await context.Resellers.AddAsync(entity);

            await Save();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}