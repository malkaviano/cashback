using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IResellerRepository
    {
        Task<Reseller> Get(string key);
        Task Create(Reseller entity);
        Task Save();
    }
}