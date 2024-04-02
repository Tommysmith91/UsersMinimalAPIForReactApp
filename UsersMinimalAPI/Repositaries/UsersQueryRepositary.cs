using Microsoft.EntityFrameworkCore;
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

        public async Task<IResponseDataModel<IEnumerable<User>>> GetAllUsers()
        {
            return new ResponseDataModel<IEnumerable<User>>()
            {
                Data = await _dbContext.Users.ToListAsync(),
                IsSuccess = true
            };
            
        }
        public async Task<IResponseDataModel<User>> GetUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return new ResponseDataModel<User>
                {
                    IsSuccess = false,
                    Message = "User Not Found"
                };
            }
            return new ResponseDataModel<User>
            {
                IsSuccess = true,
                Data = user
            };
            
        }
        public async Task<IResponseDataModel<User>> GetUser(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x=> x.Email == email);
            if (user == null)
            {
                return new ResponseDataModel<User>
                {
                    IsSuccess = false,
                    Message = "User Not Found"
                };
            }
            return new ResponseDataModel<User>
            {
                IsSuccess = true,
                Data = user
            };            
        }


    }
}
