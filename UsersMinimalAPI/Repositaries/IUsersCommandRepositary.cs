﻿using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Repositaries
{
    public interface IUsersCommandRepositary
    {
        public Task<IResponseDataModel<User>> CreateUser(User user);
        public Task<IResponseModel> DeleteUser(int id);
        public Task<IResponseModel> UpdateUser(User user, int id);
    }
}
