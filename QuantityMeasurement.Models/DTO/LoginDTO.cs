using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurement.Models
{
    // UC18: Login input DTO.
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
}
