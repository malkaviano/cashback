using System;
using System.Linq;

namespace Domain.Models {
    public class Reseller {
        private string cpf;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf {
            get { return cpf; }
            set {
                cpf = new String(value.Where(Char.IsDigit).ToArray());
            }
        }
        public string Email { get; set; }
    }
}