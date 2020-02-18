using System;
using System.Linq;
using Domain.Values;

namespace Domain.Models
{
    public class Sales
    {
        private string cpf;

        public int Id { get; set; }

        public string Code { get; set; }

        // TODO: Remove duplicated code. See Reseller
        public string Cpf
        {
            get { return cpf; }
            set
            {
                cpf = new String(value?.Where(Char.IsDigit).ToArray());
            }
        }

        public decimal? Value { get; set; }

        public DateTime Data { get; set; }

        public string Status { get; set; }

        public Reseller Reseller { get; set; }
    }
}