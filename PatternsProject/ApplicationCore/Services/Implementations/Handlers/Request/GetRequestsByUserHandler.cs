using ApplicationCore.CQRS.Queries.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class GetRequestsByUserHandler(IRequestService service) : IRequestHandler<GetRequestsByUserQuery, IEnumerable<RequestModel>>
{
    public async Task<IEnumerable<RequestModel>> HandleAsync(GetRequestsByUserQuery query)
    {
        return service.GetAll(u => u.UserId == query.UserId, asNoTracking: true);
    }
}