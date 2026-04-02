using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurement.Models
{
    // UC18: Registration input DTO.
    public class RegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = "";
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "";
    }
}
