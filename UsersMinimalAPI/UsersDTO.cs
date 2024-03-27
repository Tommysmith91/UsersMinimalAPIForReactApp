using UsersMinimalAPI.Entities;

public class UsersDTO
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set;} = string.Empty;
    public DateTime RegistrationDate { get; set; }

    

    public UsersDTO() { }
    public UsersDTO(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Password = user.PasswordHash;
        RegistrationDate = user.RegistrationDate;        
    }
}