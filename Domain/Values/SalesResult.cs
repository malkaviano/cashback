using System;

namespace Domain.Values
{
    public class SalesResult
    {
        public string Code { get; internal set; }
        public decimal Value { get; internal set; }
        public DateTime Data { get; internal set; }
        public string Cpf { get; internal set; }
        public string Status { get; set; }
        public decimal CashbackValue { get; internal set; }
        public int CashbackPercentage { get; internal set; }
    }
}