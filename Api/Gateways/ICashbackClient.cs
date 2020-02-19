using Api.Dtos;
using System.Threading.Tasks;

namespace Api.Gateways
{
    public interface ICashbackClient
    {
        Task<CashbackGet> GetCashback(string cpf);
    }
}