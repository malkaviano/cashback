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

            var result = strategy.CashbackValue(entity.Value);

            entity.CashbackValue = result.cashback;
            entity.CashbackPercentage = result.percentage;

            await salesRepo.Create(entity);
        }

        public async Task<Sales[]> Get()
        {
            return await salesRepo.Get();
        }

        public async Task Update(Sales entity)
        {
            var sales = await salesRepo.Find(entity.Id);

            if (sales == null)
            {
                throw new Exception("Sales not found");
            }

            if (sales.Status == SalesStatus.VALIDATING)
            {
                throw new Exception("Sales cannot be edit");
            }

            var newSales = Mapping.Mapper.Map(entity, sales);

            var result = strategy.CashbackValue(newSales.Value);

            newSales.CashbackValue = result.cashback;
            newSales.CashbackPercentage = result.percentage;

            await salesRepo.Update(newSales);
        }

        public async Task Delete(int id)
        {
            var sales = await salesRepo.Find(id);

            if (sales == null)
            {
                throw new Exception("Sales not found");
            }

            if (sales.Status == SalesStatus.VALIDATING)
            {
                throw new Exception("Sales cannot be deleted");
            }

            await salesRepo.Delete(sales);
        }
    }
}