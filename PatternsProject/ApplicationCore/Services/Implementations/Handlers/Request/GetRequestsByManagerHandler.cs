using ApplicationCore.CQRS.Queries.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class GetRequestsByManagerHandler(IRequestService service) : IRequestHandler<GetRequestsByManagerQuery, IEnumerable<RequestModel>>
{
    public async Task<IEnumerable<RequestModel>> HandleAsync(GetRequestsByManagerQuery query)
    {
        return service.GetAll(u => u.ManagerId == query.ManagerId, asNoTracking: true);
    }
}