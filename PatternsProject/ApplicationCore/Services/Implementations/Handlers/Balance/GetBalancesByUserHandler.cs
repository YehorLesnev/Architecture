using ApplicationCore.CQRS.Queries.Balance;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Balance;

public class GetBalancesByUserHandler(IBalanceService service) : IRequestHandler<GetBalancesByUserQuery, IEnumerable<BalanceModel>>
{
    public async Task<IEnumerable<BalanceModel>> HandleAsync(GetBalancesByUserQuery query)
    {
        return service.GetAll(u => u.UserId == query.UserId, asNoTracking: true);
    }
}