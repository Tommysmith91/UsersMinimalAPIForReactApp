using MediatR;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class AuthoriseUserCommand : IRequest<IResponseModel>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
    }
}
