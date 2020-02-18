using EFRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Threading.Tasks;

namespace Api.Services {
    public class ResellerService: IService<Reseller> {
        private readonly IRepository<Reseller> repository;

        public ResellerService(IRepository<Reseller> repository)
        {
            this.repository = repository;
        }

        public async Task Create(Reseller entity)
        {
            await repository.Create(entity);
        }

        public async Task<Reseller> GetByCpf(string cpf)
        {
            return await repository.Get(cpf);
        }
    }
}