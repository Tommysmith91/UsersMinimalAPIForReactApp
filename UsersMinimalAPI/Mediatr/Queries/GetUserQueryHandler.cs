using MediatR;
using UsersMinimalAPI.Entities;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Mediatr.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IResponseDataModel<User>>
    {
        private readonly IUsersQueryRepositary _usersQueryRepositary;
        public GetUserQueryHandler(IUsersQueryRepositary usersQueryRepositary)
        {
            _usersQueryRepositary = usersQueryRepositary;
        }
        public async Task<IResponseDataModel<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _usersQueryRepositary.GetUser(request.Id);
        }
    }
}
