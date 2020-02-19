using Domain.Interfaces;
using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services {
    public class ResellerService: IResellerService {
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