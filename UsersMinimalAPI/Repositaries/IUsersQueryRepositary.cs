using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Repositaries
{
    public interface IUsersQueryRepositary
    {
        List<User> GetAllUsers();
        User GetUser(int id);
    }
}
