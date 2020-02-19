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

        public SalesService(
            ISalesRepository salesRepo,
            IMapper mapper
        )
        {
            this.salesRepo = salesRepo;
            this.mapper = mapper;
        }

        public async Task Create(Sales entity)
        {
            entity.Status = entity.Cpf == "15350946056" ?
                                            SalesStatus.APPROVED :
                                            SalesStatus.VALIDATING;

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

            if (sales.Status == SalesStatus.VALIDATING) {
                throw new Exception("Sales cannot be edit");
            }

            var newSales = Mapping.Mapper.Map(entity, sales);

            await salesRepo.Update(newSales);
        }

        public async Task Delete(int id)
        {
            var sales = await salesRepo.Find(id);

            if (sales == null)
            {
                throw new Exception("Sales not found");
            }

            if (sales.Status == SalesStatus.VALIDATING) {
                throw new Exception("Sales cannot be deleted");
            }

            await salesRepo.Delete(sales);
        }
    }
}