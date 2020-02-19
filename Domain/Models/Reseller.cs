using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain.Models {
    public class Reseller : Identifiable {
        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Sales> Sales { get; set; }
    }
}