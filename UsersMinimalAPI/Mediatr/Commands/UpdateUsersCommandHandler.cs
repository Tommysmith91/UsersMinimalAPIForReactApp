using MediatR;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Mediatr.Commands
{
    public class UpdateUsersCommandHandler : IRequestHandler<UpdateUserCommand, IResponseModel>
    {
        private readonly IUsersCommandRepositary _usersCommandRepositary;
        public UpdateUsersCommandHandler(IUsersCommandRepositary usersCommandRepositary)
        {
            _usersCommandRepositary = usersCommandRepositary;
        }
        public async Task<IResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //TODO: Validate here before update. 
            return await _usersCommandRepositary.UpdateUser(new Entities.User(request.UsersDTO), request.Id);
        }
    }
}
