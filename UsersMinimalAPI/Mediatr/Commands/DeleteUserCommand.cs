using MediatR;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class DeleteUserCommand : IRequest<IResponseModel>
    {
        public int Id { get; set; }
    }
}
