using MediatR;
using System.Runtime.InteropServices;
using UsersMinimalAPI.Authentication;
using UsersMinimalAPI.Entities;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResponseDataModel<User>>
    {
        private readonly IUsersCommandRepositary _usersCommandRepositary;
        private readonly IPasswordHasher _passwordHasher;
        public CreateUserCommandHandler(IUsersCommandRepositary usersCommandRepositary,IPasswordHasher passwordHasher)
        {
            _usersCommandRepositary = usersCommandRepositary;
            _passwordHasher = passwordHasher;
        }
        public async Task<IResponseDataModel<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = new User(request.UsersDTO, _passwordHasher.HashPassword(request.UsersDTO.Password));
            //TODO: create validator and validate here
            return await _usersCommandRepositary.CreateUser(userEntity);
        }
    }
}
