using MediatR;
using UsersMinimalAPI.Authentication;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Mediatr.Queries
{
    public class AuthoriseUserQueryHandler : IRequestHandler<AuthoriseUserQuery, IResponseDataModel<AuthDTO>>
    {
        private IUsersQueryRepositary _usersQueryRepositary;
        private IPasswordHasher _passwordHasher;
        private IJWTTokenGenerator _jwtTokenGenerator;
        public AuthoriseUserQueryHandler(IUsersQueryRepositary usersQueryRepositary, IPasswordHasher passwordHasher, IJWTTokenGenerator jwtTokenGenerator)
        {
            _usersQueryRepositary = usersQueryRepositary;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<IResponseDataModel<AuthDTO>> Handle(AuthoriseUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _usersQueryRepositary.GetUser(request.Email);
            if(result.IsSuccess == false)
            {
                return new ResponseDataModel<AuthDTO>()
                {
                    Message = result.Message,
                    IsSuccess = false
                };
            }
            var isAuth = _passwordHasher.VerifyPassword(request.Password, result.Data.PasswordHash);
            if (isAuth == false)
            {
                return new ResponseDataModel<AuthDTO>()
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }
            return new ResponseDataModel<AuthDTO>()
            {
                IsSuccess = true,
                Data = new AuthDTO 
                { 
                    UsersDTO = new UsersDTO
                    {
                        Email = result.Data.Email,
                        CompanyName = result.Data.CompanyName,
                        Id = result.Data.Id
                    },
                    Token = _jwtTokenGenerator.GenerateToken(result.Data.Email)
                }
            };

            
        }
    }
}
