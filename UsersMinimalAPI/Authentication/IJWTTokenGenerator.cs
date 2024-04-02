namespace UsersMinimalAPI.Authentication
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(string username);
    }
}
