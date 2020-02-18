using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface ISalesRepository
    {
        Task<Sales[]> Get();
        Task<Sales> Find(int id);
        Task Create(Sales entity);
        Task Update(Sales entity);
        Task Delete(int id);
        Task Save();
    }
}