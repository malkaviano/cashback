using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class CashbackGet
    {
        public Dictionary<string, decimal> body { get; set; }
        public int statusCode { get; set; }
    }
}