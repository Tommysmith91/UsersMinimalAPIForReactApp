using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Repositaries
{
    public class UsersQueryRepositary : IUsersQueryRepositary
    {
        private readonly UsersDbContext _dbContext;
        public UsersQueryRepositary(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }
        public User GetUser(int id)
        {
            return _dbContext.Users.Find(id);
        }

    }
}
