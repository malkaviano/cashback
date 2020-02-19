using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IResellerService
    {
        Task<Reseller> GetByCpf(string cpf);
        Task Create(Reseller entity);
    }
}