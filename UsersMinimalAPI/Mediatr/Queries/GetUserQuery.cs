using MediatR;
using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI.Mediatr.Queries
{
    public class GetUserQuery : IRequest<IResponseDataModel<User>>
    {
        public int Id { get; set; }
    }
}
