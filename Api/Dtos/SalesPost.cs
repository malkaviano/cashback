using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class SalesPost
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Code { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required]
        [Range(0.01, 1000000000)]
        public decimal Value { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
    }
}
