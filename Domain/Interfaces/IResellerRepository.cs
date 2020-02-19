using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IResellerRepository
    {
        Task<Reseller> GetByCpf(string cpf);
        Task<Reseller> GetByEmail(string email);
        Task Create(Reseller entity);
        Task Save();
    }
}