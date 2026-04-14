using Microsoft.AspNetCore.Mvc;
using BusinessLogic.API.Models;
using BusinessLogic.API.Services;
namespace BusinessLogic.API.Controllers
{
    // UC21: Authentication controller — routes through API Gateway.
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
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            AuthResponseDTO response = await authService.RegisterAsync(registerDTO);
            return Ok(response);
        }
        // POST /api/v1/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            AuthResponseDTO response = await authService.LoginAsync(loginDTO);
            return Ok(response);
        }
    }
}
