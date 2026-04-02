using QuantityMeasurement.Models;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service.Security;
namespace QuantityMeasurement.Service
{
    // UC18: Authentication service implementation.
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly PasswordHasher passwordHasher;
        private readonly JwtTokenGenerator jwtTokenGenerator;
        private readonly AesEncryptionService aesEncryptionService;

        public AuthServiceImpl(IUserRepository userRepository, PasswordHasher passwordHasher,
            JwtTokenGenerator jwtTokenGenerator, AesEncryptionService aesEncryptionService)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.aesEncryptionService = aesEncryptionService;
        }
        // Register a new user
        public AuthResponseDTO Register(RegisterDTO registerDTO)
        {
            // Check for duplicate username
            if (userRepository.ExistsByUsername(registerDTO.Username))
            {
                throw new QuantityMeasurementException("Username already exists: " + registerDTO.Username);
            }
            // Encrypt email and check for duplicate
            string encryptedEmail = aesEncryptionService.Encrypt(registerDTO.Email);
            // Generate salt and hash password
            string salt = passwordHasher.GenerateSalt();
            string passwordHash = passwordHasher.HashPassword(registerDTO.Password, salt);
            // Create and save user entity
            UserEntity user = new UserEntity(registerDTO.Username, encryptedEmail, passwordHash, salt);
            userRepository.Save(user);
            // Generate JWT token
            string token = jwtTokenGenerator.GenerateToken(user);
            DateTime expiresAt = jwtTokenGenerator.GetExpirationTime();
            return new AuthResponseDTO(token, user.Username, expiresAt);
        }
        // Login with existing credentials
        public AuthResponseDTO Login(LoginDTO loginDTO)
        {
            // Find user by username
            UserEntity user = userRepository.FindByUsername(loginDTO.Username);
            if (user == null)
            {
                throw new QuantityMeasurementException("Invalid username or password.");
            }
            // Verify password hash
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
