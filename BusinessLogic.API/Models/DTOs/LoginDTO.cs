using System.ComponentModel.DataAnnotations;
namespace BusinessLogic.API.Models
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
