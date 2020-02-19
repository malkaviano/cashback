using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IResellerService
    {
        Task<Reseller> GetByCpf(string cpf);
        Task<Reseller> GetByEmail(string email);
        Task Create(Reseller entity);
    }
}