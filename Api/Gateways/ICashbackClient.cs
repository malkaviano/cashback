using Api.Dtos;
using System.Threading.Tasks;

namespace Api.Gateways
{
    public interface ICashbackClient
    {
        Task<CashbackDto> GetCashback(string cpf);
    }
}