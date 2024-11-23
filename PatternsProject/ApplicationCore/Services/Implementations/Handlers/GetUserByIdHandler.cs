using ApplicationCore.CQRS.Queries;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers;

public class GetUserByIdHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, UserModel>
{
	public async Task<UserModel?> HandleAsync(GetUserByIdQuery query)
    {
        return await userService.GetAsync(u => u.Id == query.UserId, asNoTracking: true);
    }
}

