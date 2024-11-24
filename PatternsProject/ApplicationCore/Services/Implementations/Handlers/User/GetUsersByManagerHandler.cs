using ApplicationCore.CQRS.Queries.User;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.User;

public class GetUsersByManagerHandler(IUserService userService) : IRequestHandler<GetUsersByManagerQuery, IEnumerable<UserModel>>
{
    public async Task<IEnumerable<UserModel>> HandleAsync(GetUsersByManagerQuery query)
    {
        return userService.GetAll(u => u.ManagerId == query.ManagerId, asNoTracking: true);
    }
}