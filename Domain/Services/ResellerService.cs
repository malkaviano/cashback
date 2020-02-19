using Domain.Interfaces;
using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services {
    public class ResellerService: IResellerService {
        private readonly IResellerRepository repository;

        public ResellerService(IResellerRepository repository)
        {
            this.repository = repository;
        }

        public async Task Create(Reseller entity)
        {
            await repository.Create(entity);
        }

        public async Task<Reseller> GetByCpf(string cpf)
        {
            return await repository.GetByCpf(cpf);
        }

        public async Task<Reseller> GetByEmail(string email)
        {
            return await repository.GetByEmail(email);
        }
    }
}