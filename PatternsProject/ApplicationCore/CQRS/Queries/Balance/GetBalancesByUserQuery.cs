using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.Balance;

public class GetBalancesByUserQuery : IRequest<IEnumerable<BalanceModel>>
{
    public Guid UserId { get; set; }
}
