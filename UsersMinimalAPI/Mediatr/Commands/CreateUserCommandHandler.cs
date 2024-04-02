using FluentValidation;
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
        private readonly IValidator<UsersDTO> _validator;
        public CreateUserCommandHandler(IUsersCommandRepositary usersCommandRepositary,IPasswordHasher passwordHasher,IValidator<UsersDTO> validator)
        {
            _usersCommandRepositary = usersCommandRepositary;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }
        public async Task<IResponseDataModel<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //I've validated the DTO here as it's a password at this point. It gets hashed below, we want to check the non-hashed value.
            var validationResult = _validator.Validate(request.UsersDTO);
            if(validationResult.IsValid == false)
            {
                return new ResponseDataModel<User>()
                {
                    IsSuccess = false,
                    Message = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage))                    
                };
            }
            var userEntity = new User(request.UsersDTO, _passwordHasher.HashPassword(request.UsersDTO.Password));
            
            return await _usersCommandRepositary.CreateUser(userEntity);
        }
    }
}
