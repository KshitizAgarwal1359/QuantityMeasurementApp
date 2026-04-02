using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.Models;
using QuantityMeasurement.Service;
namespace QuantityMeasurement.Controllers
{
    // UC18: Authentication controller for register and login.
    [ApiController]
    [Route("api/v1/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        // POST /api/v1/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            AuthResponseDTO response = authService.Register(registerDTO);
            return Ok(response);
        }
        // POST /api/v1/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            AuthResponseDTO response = authService.Login(loginDTO);
            return Ok(response);
        }
    }
}
