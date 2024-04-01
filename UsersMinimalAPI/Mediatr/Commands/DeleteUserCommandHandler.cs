using MediatR;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResponseModel>
    {
        private readonly IUsersCommandRepositary _usersCommandRepositary;
        public DeleteUserCommandHandler(IUsersCommandRepositary usersCommandRepositary) 
        { 
            _usersCommandRepositary= usersCommandRepositary;        
        }
        public async Task<IResponseModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _usersCommandRepositary.DeleteUser(request.Id);
        }
    }
}
