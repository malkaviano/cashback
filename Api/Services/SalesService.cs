using System.Linq;
using EFRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Threading.Tasks;
using System;
using Domain.Values;
using AutoMapper;

namespace Api.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository salesRepo;
        private readonly IRepository<Reseller> resellerRepo;
        private readonly IMapper mapper;

        public SalesService(
            ISalesRepository salesRepo,
            IRepository<Reseller> resellerRepo,
            IMapper mapper
        )
        {
            this.salesRepo = salesRepo;
            this.resellerRepo = resellerRepo;
            this.mapper = mapper;
        }

        public async Task Create(Sales entity)
        {
            // TODO: Move to repository
            var reseller = await resellerRepo.Get(entity.Cpf);

            if (reseller == null)
            {
                throw new Exception("CPF not found");
            }

            entity.Reseller = reseller;

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

            var newSales = mapper.Map(entity, sales);

            await salesRepo.Update(newSales);
        }

        public async Task Delete(int id)
        {
            await salesRepo.Delete(id);
        }
    }
}