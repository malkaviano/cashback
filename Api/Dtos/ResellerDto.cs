using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class ResellerDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
