using System.ComponentModel.DataAnnotations;

namespace UsersMinimalAPI.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
