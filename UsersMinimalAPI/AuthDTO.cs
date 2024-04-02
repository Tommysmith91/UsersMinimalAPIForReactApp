namespace UsersMinimalAPI
{
    public class AuthDTO
    {
        public string Token { get; set; } = string.Empty;
        public UsersDTO UsersDTO { get; set; }

    }
}
