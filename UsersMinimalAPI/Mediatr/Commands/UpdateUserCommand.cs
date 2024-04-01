using MediatR;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class UpdateUserCommand : IRequest<IResponseModel>
    {
        public UsersDTO UsersDTO { get; set; }
        public int Id { get; set; }
    }
}
