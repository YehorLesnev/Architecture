using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.Balance;

public class UpdateBalanceCommand : IRequest
{
    public Guid BalanceId { get; set; }
    public string Type { get; set; }
    public decimal BalanceAmount { get; set; }
    public Guid UserId { get; set; }
}