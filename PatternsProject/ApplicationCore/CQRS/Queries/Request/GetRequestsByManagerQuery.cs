using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.Request;

public class GetRequestsByManagerQuery : IRequest<IEnumerable<RequestModel>>
{
    public Guid ManagerId { get; set; }
}