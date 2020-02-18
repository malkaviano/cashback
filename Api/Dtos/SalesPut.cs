using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class SalesPut
    {
        [StringLength(20, MinimumLength = 5)]
        public string Code { get; set; }

        [StringLength(14, MinimumLength = 11)]
        public string Cpf { get; set; }

        [Range(0.01, 1000000000)]
        public decimal? Value { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
    }
}
