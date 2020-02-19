using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using System.Threading.Tasks;
using System;
using Domain.Values;
using AutoMapper;

namespace Domain.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository salesRepo;
        private readonly IMapper mapper;
        private readonly ICashbackStrategy strategy;

        public SalesService(
            ISalesRepository salesRepo,
            IMapper mapper,
            ICashbackStrategy strategy
        )
        {
            this.salesRepo = salesRepo;
            this.mapper = mapper;
            this.strategy = strategy;
        }

        public async Task Create(Sales entity)
        {
            entity.Status = entity.Cpf == "15350946056" ?
                                            SalesStatus.APPROVED :
                                            SalesStatus.VALIDATING;

            strategy.Apply(entity);

            await salesRepo.Create(entity);
        }
        public async Task Update(string code, decimal value, DateTime data)
        {
            var sales = await salesRepo.FindByCode(code);

            if (sales == null)
            {
                throw new Exception("Sales not found");
            }

            if (sales.Status != SalesStatus.VALIDATING)
            {
                throw new Exception("Sales cannot be edit");
            }

            sales.Data = data;
            sales.Value = value;

            strategy.Apply(sales);

            await salesRepo.Update(sales);
        }

        public async Task Delete(string code)
        {
            var sales = await salesRepo.FindByCode(code);

            if (sales == null)
            {
                throw new Exception("Sales not found");
            }

            if (sales.Status != SalesStatus.VALIDATING)
            {
                throw new Exception("Sales cannot be deleted");
            }

            await salesRepo.Delete(sales);
        }

        public async Task<Sales[]> GetByCpf(string cpf)
        {
            return await salesRepo.GetByCpf(cpf);
        }
    }
}