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

        public User()
        {
        }
        public User(UsersDTO userDTO)
        {
            Id = userDTO.Id;
            Email = userDTO.Email;
            RegistrationDate = userDTO.RegistrationDate;            
        }
        public User(UsersDTO userDTO, string passwordHash)
        {
            Id = userDTO.Id;
            Email = userDTO.Email;
            PasswordHash = passwordHash;
            RegistrationDate = userDTO.RegistrationDate;
        }
        
    }
}
