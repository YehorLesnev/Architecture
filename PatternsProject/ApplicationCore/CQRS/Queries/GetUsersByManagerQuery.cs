using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries;

public class GetUsersByManagerQuery : IRequest<IEnumerable<UserModel>>
{
    public Guid ManagerId { get; set; }
}
