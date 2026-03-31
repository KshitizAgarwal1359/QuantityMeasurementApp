using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuantityMeasurement.Models;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service;
using QuantityMeasurement.Service.Security;
namespace QuantityMeasurement.Tests
{
    // UC18: tests for user authentication features.
    public class UserAuthenticationTest
    {
        private readonly PasswordHasher passwordHasher;
        private readonly AesEncryptionService aesEncryptionService;
        public UserAuthenticationTest()
        {
            passwordHasher = new PasswordHasher();
            // 32-byte test key for AES-256
            byte[] testKey = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                testKey[i] = (byte)(i + 1);
            }
            aesEncryptionService = new AesEncryptionService(testKey);
        }
        // ---- Password Hashing Tests ----
        [Fact]
        public void Test_GenerateSalt_ReturnsNonEmptyString()
        {
            string salt = passwordHasher.GenerateSalt();
            Assert.NotNull(salt);
            Assert.True(salt.Length > 0);
        }
        [Fact]
        public void Test_GenerateSalt_ReturnsDifferentSaltsEachTime()
        {
            string salt1 = passwordHasher.GenerateSalt();
            string salt2 = passwordHasher.GenerateSalt();
            Assert.NotEqual(salt1, salt2);
        }
        [Fact]
        public void Test_HashPassword_ReturnsConsistentHash()
        {
            string salt = passwordHasher.GenerateSalt();
            string hash1 = passwordHasher.HashPassword("MyPassword123", salt);
            string hash2 = passwordHasher.HashPassword("MyPassword123", salt);
            Assert.Equal(hash1, hash2);
        }
        [Fact]
        public void Test_HashPassword_DifferentSaltsProduceDifferentHashes()
        {
            string salt1 = passwordHasher.GenerateSalt();
            string salt2 = passwordHasher.GenerateSalt();
            string hash1 = passwordHasher.HashPassword("MyPassword123", salt1);
            string hash2 = passwordHasher.HashPassword("MyPassword123", salt2);
            Assert.NotEqual(hash1, hash2);
        }
        [Fact]
        public void Test_VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            string salt = passwordHasher.GenerateSalt();
            string hash = passwordHasher.HashPassword("CorrectPassword", salt);
            bool result = passwordHasher.VerifyPassword("CorrectPassword", salt, hash);
            Assert.True(result);
        }
        [Fact]
        public void Test_VerifyPassword_WrongPassword_ReturnsFalse()
        {
            string salt = passwordHasher.GenerateSalt();
            string hash = passwordHasher.HashPassword("CorrectPassword", salt);
            bool result = passwordHasher.VerifyPassword("WrongPassword", salt, hash);
            Assert.False(result);
        }
        // ---- AES Encryption Tests ----
        [Fact]
        public void Test_Encrypt_ReturnsNonEmptyString()
        {
            string encrypted = aesEncryptionService.Encrypt("test@email.com");
            Assert.NotNull(encrypted);
            Assert.True(encrypted.Length > 0);
        }
        [Fact]
        public void Test_Encrypt_ProducesDifferentOutputFromInput()
        {
            string plainText = "test@email.com";
            string encrypted = aesEncryptionService.Encrypt(plainText);
            Assert.NotEqual(plainText, encrypted);
        }
        [Fact]
        public void Test_Decrypt_ReturnsOriginalText()
        {
            string original = "test@email.com";
            string encrypted = aesEncryptionService.Encrypt(original);
            string decrypted = aesEncryptionService.Decrypt(encrypted);
            Assert.Equal(original, decrypted);
        }
        [Fact]
        public void Test_EncryptDecrypt_RoundTrip_MultipleInputs()
        {
            string[] inputs = { "hello@world.com", "user123", "special!@#$%chars" };
            foreach (string input in inputs)
            {
                string encrypted = aesEncryptionService.Encrypt(input);
                string decrypted = aesEncryptionService.Decrypt(encrypted);
                Assert.Equal(input, decrypted);
            }
        }
        // ---- User Entity Tests ----
        [Fact]
        public void Test_UserEntity_DefaultValues()
        {
            UserEntity user = new UserEntity();
            Assert.Equal(0, user.Id);
            Assert.Equal("", user.Username);
            Assert.Equal("", user.Email);
            Assert.Equal("", user.PasswordHash);
            Assert.Equal("", user.Salt);
        }
        [Fact]
        public void Test_UserEntity_PropertyAssignment()
        {
            UserEntity user = new UserEntity("testuser", "encrypted@email", "hash123", "salt123");
            Assert.Equal("testuser", user.Username);
            Assert.Equal("encrypted@email", user.Email);
            Assert.Equal("hash123", user.PasswordHash);
            Assert.Equal("salt123", user.Salt);
        }
        // ---- Auth Service Tests (with InMemory DB) ----
        private QuantityMeasurementDbContext CreateInMemoryContext(string dbName)
        {
            DbContextOptionsBuilder<QuantityMeasurementDbContext> optionsBuilder =
                new DbContextOptionsBuilder<QuantityMeasurementDbContext>();
            optionsBuilder.UseInMemoryDatabase(dbName);
            return new QuantityMeasurementDbContext(optionsBuilder.Options);
        }
        private IConfiguration CreateTestConfiguration()
        {
            Dictionary<string, string> configValues = new Dictionary<string, string>();
            configValues.Add("Jwt:SecretKey", "TestSecretKeyThatIsAtLeast32BytesLongForHmac!!");
            configValues.Add("Jwt:Issuer", "TestIssuer");
            configValues.Add("Jwt:Audience", "TestAudience");
            configValues.Add("Jwt:ExpirationMinutes", "60");
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(configValues!);
            return configBuilder.Build();
        }
        private AuthServiceImpl CreateAuthService(string dbName)
        {
            QuantityMeasurementDbContext context = CreateInMemoryContext(dbName);
            UserRepository userRepository = new UserRepository(context);
            IConfiguration config = CreateTestConfiguration();
            JwtTokenGenerator jwtGenerator = new JwtTokenGenerator(config);
            byte[] testKey = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                testKey[i] = (byte)(i + 1);
            }
            AesEncryptionService aes = new AesEncryptionService(testKey);
            return new AuthServiceImpl(userRepository, passwordHasher, jwtGenerator, aes);
        }
        [Fact]
        public void Test_Register_NewUser_ReturnsToken()
        {
            AuthServiceImpl authService = CreateAuthService("RegisterTest1");
            RegisterDTO dto = new RegisterDTO();
            dto.Username = "newuser";
            dto.Email = "new@test.com";
            dto.Password = "Password123";
            AuthResponseDTO response = authService.Register(dto);
            Assert.NotNull(response.Token);
            Assert.True(response.Token.Length > 0);
            Assert.Equal("newuser", response.Username);
        }
        [Fact]
        public void Test_Register_DuplicateUsername_ThrowsException()
        {
            AuthServiceImpl authService = CreateAuthService("RegisterTest2");
            RegisterDTO dto = new RegisterDTO();
            dto.Username = "duplicate";
            dto.Email = "first@test.com";
            dto.Password = "Password123";
            authService.Register(dto);
            RegisterDTO dto2 = new RegisterDTO();
            dto2.Username = "duplicate";
            dto2.Email = "second@test.com";
            dto2.Password = "Password456";
            Assert.Throws<QuantityMeasurementException>(
                () => { authService.Register(dto2); });
        }
        [Fact]
        public void Test_Login_CorrectPassword_ReturnsToken()
        {
            AuthServiceImpl authService = CreateAuthService("LoginTest1");
            RegisterDTO registerDto = new RegisterDTO();
            registerDto.Username = "loginuser";
            registerDto.Email = "login@test.com";
            registerDto.Password = "MyPassword123";
            authService.Register(registerDto);
            LoginDTO loginDto = new LoginDTO();
            loginDto.Username = "loginuser";
            loginDto.Password = "MyPassword123";
            AuthResponseDTO response = authService.Login(loginDto);
            Assert.NotNull(response.Token);
            Assert.True(response.Token.Length > 0);
            Assert.Equal("loginuser", response.Username);
        }
        [Fact]
        public void Test_Login_WrongPassword_ThrowsException()
        {
            AuthServiceImpl authService = CreateAuthService("LoginTest2");
            RegisterDTO registerDto = new RegisterDTO();
            registerDto.Username = "wrongpwuser";
            registerDto.Email = "wrongpw@test.com";
            registerDto.Password = "CorrectPassword";
            authService.Register(registerDto);
            LoginDTO loginDto = new LoginDTO();
            loginDto.Username = "wrongpwuser";
            loginDto.Password = "WrongPassword";
            Assert.Throws<QuantityMeasurementException>(
                () => { authService.Login(loginDto); });
        }
        [Fact]
        public void Test_Login_NonExistentUser_ThrowsException()
        {
            AuthServiceImpl authService = CreateAuthService("LoginTest3");
            LoginDTO loginDto = new LoginDTO();
            loginDto.Username = "ghostuser";
            loginDto.Password = "AnyPassword";
            Assert.Throws<QuantityMeasurementException>(
                () => { authService.Login(loginDto); });
        }
        // ---- JWT Token Tests ----
        [Fact]
        public void Test_GenerateToken_ReturnsNonEmptyString()
        {
            IConfiguration config = CreateTestConfiguration();
            JwtTokenGenerator generator = new JwtTokenGenerator(config);
            UserEntity user = new UserEntity("testuser", "test@email.com", "hash", "salt");

            string token = generator.GenerateToken(user);

            Assert.NotNull(token);
            Assert.True(token.Length > 0);
        }
        [Fact]
        public void Test_GenerateToken_ContainsDotSeparators()
        {
            IConfiguration config = CreateTestConfiguration();
            JwtTokenGenerator generator = new JwtTokenGenerator(config);
            UserEntity user = new UserEntity("testuser", "test@email.com", "hash", "salt");
            string token = generator.GenerateToken(user);
            // JWT tokens have 3 parts separated by dots
            string[] parts = token.Split('.');
            Assert.Equal(3, parts.Length);
        }
    }
}
