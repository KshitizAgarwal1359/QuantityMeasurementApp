using BusinessLogic.API.Models;
namespace BusinessLogic.API.Services
{
    // UC21: Auth service interface — async for interservice communication.
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
    }
}
