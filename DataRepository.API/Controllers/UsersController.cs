using Microsoft.AspNetCore.Mvc;
using DataRepository.API.Models;

namespace DataRepository.API.Controllers
{
    // UC21: Internal CRUD controller for User entities.
    // Called by BusinessLogic.API via HttpClient (Interservice Communication).
    [ApiController]
    [Route("api/data/users")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // POST /api/data/users — Save a new user
        [HttpPost]
        public IActionResult Save([FromBody] UserEntity user)
        {
            UserEntity saved = userRepository.Save(user);
            return Ok(saved);
        }

        // GET /api/data/users/username/{username} — Find user by username
        [HttpGet("username/{username}")]
        public IActionResult FindByUsername(string username)
        {
            UserEntity user = userRepository.FindByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET /api/data/users/exists/{username} — Check if username exists
        [HttpGet("exists/{username}")]
        public IActionResult ExistsByUsername(string username)
        {
            bool exists = userRepository.ExistsByUsername(username);
            return Ok(exists);
        }
    }
}
