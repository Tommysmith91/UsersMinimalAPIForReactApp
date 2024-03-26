using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Repositaries
{
    public class UsersCommandRepositary : IUsersCommandRepositary
    {
        private readonly UsersDbContext _dbContext;
        public UsersCommandRepositary(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User CreateUser(User user)
        {
            _dbContext.Users.AddAsync(user);
            _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IResponseModel> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "User Not Found"
                };
            }
            _dbContext.Users.Remove(user);  
            await _dbContext.SaveChangesAsync();
            return new ResponseModel
            {
                IsSuccess = true
            };
        }

        public async Task<IResponseModel> UpdateUser(User user, int id)
        {
            var existingUser = await _dbContext.Users.FindAsync(id);
            if(existingUser == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "User Not Found"
                };
            }
            existingUser.Email = user.Email;            
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.RegistrationDate = user.RegistrationDate;
            await _dbContext.SaveChangesAsync();
            return new ResponseModel
            {
                IsSuccess = true
            };

        }
    }
}
