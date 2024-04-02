using UsersMinimalAPI.Entities;

public class UsersDTO
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set;} = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string CompanyName { get; set; } = string.Empty;

    public UsersDTO() { }
    public UsersDTO(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Password = user.PasswordHash;
        CompanyName = user.CompanyName;
        RegistrationDate = user.RegistrationDate;        
    }
}