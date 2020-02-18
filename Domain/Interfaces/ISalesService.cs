using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface ISalesService
    {
        Task<Sales[]> Get();
        Task Create(Sales entity);
        Task Update(Sales entity);
        Task Delete(int id);
    }
}