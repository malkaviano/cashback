using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFRepository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DataContext context;

        public SalesRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Sales[]> Get()
        {
            return await context.Sales.ToArrayAsync();
        }

        public async Task Create(Sales entity)
        {
            context.Sales.Add(entity);

            await Save();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Sales> Find(int id)
        {
            return await context.Sales.FindAsync(id);
        }

        public async Task Update(Sales entity)
        {
            context.Sales.Update(entity);

            await Save();
        }

        public async Task Delete(int id)
        {
            var result = await Find(id);

            if(result == null) {
                throw new System.Exception("Sales not found");
            }

            context.Sales.Remove(result);

            await Save();
        }
    }
}