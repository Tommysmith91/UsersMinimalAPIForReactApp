using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Repositaries
{
    public interface IUsersQueryRepositary
    {
        Task<IResponseDataModel<IEnumerable<User>>> GetAllUsers();
        Task<IResponseDataModel<User>> GetUser(int id);
    }
}
