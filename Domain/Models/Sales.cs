using System;
using System.Linq;
using Domain.Values;

namespace Domain.Models
{
    public class Sales : Identifiable
    {
        public string Code { get; set; }

        public decimal Value { get; set; }

        public DateTime Data { get; set; }

        public string Status { get; set; }

        public decimal CashbackValue { get; set; }

        public int CashbackPercentage { get; set; }

        public Reseller Reseller { get; set; }
    }
}