using System;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface ISalesService
    {
        Task<Sales[]> GetByCpf(string cpf);
        Task Create(Sales entity);
        Task Update(string code, decimal value, DateTime data);
        Task Delete(string code);
    }
}