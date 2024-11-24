using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.User;

public class GetUsersByManagerQuery : IRequest<IEnumerable<UserModel>>
{
    public Guid ManagerId { get; set; }
}
