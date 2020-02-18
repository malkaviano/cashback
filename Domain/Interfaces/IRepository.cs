using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface IRepository<T>
    {
        Task<T> Get(string key);
        Task Create(T entity);
    }
}