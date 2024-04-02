using MediatR;

namespace UsersMinimalAPI.Mediatr.Queries
{
    public class AuthoriseUserQuery : IRequest<IResponseDataModel<AuthDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
    }
}
