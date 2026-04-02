using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurement.Models
{
    // UC18: User entity mapped to Users table via EF Core.
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Username")]
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = "";

        [Column("Email")]
        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = "";

        [Column("PasswordHash")]
        [Required]
        [MaxLength(128)]
        public string PasswordHash { get; set; } = "";

        [Column("Salt")]
        [Required]
        [MaxLength(64)]
        public string Salt { get; set; } = "";

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        // Parameterless constructor for EF Core
        public UserEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        // Constructor for registration
        public UserEntity(string username, string email, string passwordHash, string salt)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"User[{Id}]: {Username}";
        }
    }
}
