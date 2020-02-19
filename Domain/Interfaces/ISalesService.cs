using System;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Values;

namespace Domain.Interfaces {
    public interface ISalesService
    {
        Task<SalesResult[]> GetByCpf(string cpf);
        Task Create(Sales entity);
        Task Update(string code, decimal value, DateTime data);
        Task Delete(string code);
    }
}