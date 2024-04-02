using MediatR;
using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Mediatr.Queries
{
    public class GetAllUsersQuery : IRequest<IResponseDataModel<IEnumerable<User>>>
    {
    }
}
