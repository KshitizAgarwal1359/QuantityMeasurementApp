using System.Net.Http.Json;
using BusinessLogic.API.Models;
using BusinessLogic.API.Security;

namespace BusinessLogic.API.Services
{
    // UC21: Auth service with Interservice Communication via HttpClient.
    // Calls DataRepository.API instead of direct database access.
    public class AuthServiceImpl : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly PasswordHasher passwordHasher;
        private readonly JwtTokenGenerator jwtTokenGenerator;
        private readonly AesEncryptionService aesEncryptionService;

        public AuthServiceImpl(IHttpClientFactory httpClientFactory, PasswordHasher passwordHasher,
            JwtTokenGenerator jwtTokenGenerator, AesEncryptionService aesEncryptionService)
        {
            this.httpClient = httpClientFactory.CreateClient("DataRepositoryAPI");
            this.passwordHasher = passwordHasher;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.aesEncryptionService = aesEncryptionService;
        }

        // Register a new user — calls DataRepository.API via HTTP
        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            // Check for duplicate username via DataRepository.API
            bool exists = await httpClient.GetFromJsonAsync<bool>($"api/data/users/exists/{registerDTO.Username}");
            if (exists)
            {
                throw new QuantityMeasurementException("Username already exists: " + registerDTO.Username);
            }

            // Encrypt email, hash password
            string encryptedEmail = aesEncryptionService.Encrypt(registerDTO.Email);
            string salt = passwordHasher.GenerateSalt();
            string passwordHash = passwordHasher.HashPassword(registerDTO.Password, salt);

            // Save user via DataRepository.API
            UserEntity user = new UserEntity(registerDTO.Username, encryptedEmail, passwordHash, salt);
            var response = await httpClient.PostAsJsonAsync("api/data/users", user);
            response.EnsureSuccessStatusCode();
            UserEntity savedUser = (await response.Content.ReadFromJsonAsync<UserEntity>())!;

            // Generate JWT token
            string token = jwtTokenGenerator.GenerateToken(savedUser);
            DateTime expiresAt = jwtTokenGenerator.GetExpirationTime();
            return new AuthResponseDTO(token, savedUser.Username, expiresAt);
        }

        // Login — calls DataRepository.API via HTTP
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            // Find user via DataRepository.API
            HttpResponseMessage response = await httpClient.GetAsync($"api/data/users/username/{loginDTO.Username}");
            if (!response.IsSuccessStatusCode)
            {
                throw new QuantityMeasurementException("Invalid username or password.");
            }

            UserEntity user = (await response.Content.ReadFromJsonAsync<UserEntity>())!;

            // Verify password
            bool isValid = passwordHasher.VerifyPassword(loginDTO.Password, user.Salt, user.PasswordHash);
            if (!isValid)
            {
                throw new QuantityMeasurementException("Invalid username or password.");
            }

            // Generate JWT token
            string token = jwtTokenGenerator.GenerateToken(user);
            DateTime expiresAt = jwtTokenGenerator.GetExpirationTime();
            return new AuthResponseDTO(token, user.Username, expiresAt);
        }
    }
}
