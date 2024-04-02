using MediatR;
using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class CreateUserCommand : IRequest<IResponseDataModel<User>>
    {
        public UsersDTO UsersDTO { get; set; }
    }
}
