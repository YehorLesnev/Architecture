using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.Request;


public class GetRequestsByUserQuery : IRequest<IEnumerable<RequestModel>>
{
    public Guid UserId { get; set; }
}