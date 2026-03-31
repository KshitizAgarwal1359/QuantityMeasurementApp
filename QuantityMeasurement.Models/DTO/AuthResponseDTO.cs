namespace QuantityMeasurement.Models
{
    // UC18: Authentication response DTO with JWT token.
    public class AuthResponseDTO
    {
        public string Token { get; set; } = "";
        public string Username { get; set; } = "";
        public DateTime ExpiresAt { get; set; }
        public AuthResponseDTO() { }
        public AuthResponseDTO(string token, string username, DateTime expiresAt)
        {
            Token = token;
            Username = username;
            ExpiresAt = expiresAt;
        }
    }
}
