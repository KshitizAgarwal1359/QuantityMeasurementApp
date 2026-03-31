using QuantityMeasurement.Models;

namespace QuantityMeasurement.Service
{
    // UC18: Authentication service interface.
    public interface IAuthService
    {
        AuthResponseDTO Register(RegisterDTO registerDTO);

        AuthResponseDTO Login(LoginDTO loginDTO);
    }
}
