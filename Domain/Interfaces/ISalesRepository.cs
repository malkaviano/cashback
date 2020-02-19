using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface ISalesRepository
    {
        Task<Sales[]> GetByCpf(string cpf);
        Task<Sales> FindByCode(string code);
        Task Create(Sales entity);
        Task Update(Sales entity);
        Task Delete(Sales entity);
        Task Save();
    }
}