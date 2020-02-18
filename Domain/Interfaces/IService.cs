using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface IService<T>
    {
        Task<T> GetByCpf(string cpf);
        Task Create(T entity);
    }
}