using MediatR;
using UsersMinimalAPI.Entities;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Mediatr.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IResponseDataModel<IEnumerable<User>>>
    {
        private IUsersQueryRepositary _usersQueryRepositary;
        public GetAllUsersQueryHandler(IUsersQueryRepositary usersQueryRepositary)
        {
            _usersQueryRepositary = usersQueryRepositary;
        }

        public async Task<IResponseDataModel<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _usersQueryRepositary.GetAllUsers();
        }
    }
}
