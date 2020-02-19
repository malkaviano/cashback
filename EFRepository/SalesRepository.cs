using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace EFRepository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DataContext context;

        public SalesRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task Create(Sales entity)
        {
            var reseller = await context.Resellers.SingleOrDefaultAsync(
                r => r.Cpf == entity.Cpf
            );

            if (reseller == null)
            {
                throw new Exception("Reseller CPF not found");
            }

            entity.Reseller = reseller;

            context.Sales.Add(entity);

            await Save();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Sales> FindByCode(string code)
        {
            return await context.Sales.Where(s => s.Code == code).SingleOrDefaultAsync();
        }

        public async Task Update(Sales entity)
        {
            context.Sales.Update(entity);

            await Save();
        }

        public async Task Delete(Sales entity)
        {
            context.Sales.Remove(entity);

            await Save();
        }

        public async Task<Sales[]> GetByCpf(string cpf)
        {
            return await context.Sales.Where(s => s.Cpf == cpf).ToArrayAsync();
        }
    }
}