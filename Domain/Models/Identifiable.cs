using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain.Models {
    public abstract class Identifiable {
        private string cpf;

        public long Id { get; set; }

        public string Cpf {
            get { return cpf; }
            set {
                cpf = new String(value?.Where(Char.IsDigit).ToArray());
            }
        }
    }
}