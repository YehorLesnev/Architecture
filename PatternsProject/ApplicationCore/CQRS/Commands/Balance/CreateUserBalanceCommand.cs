using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.Balance;

public class CreateUserBalanceCommand : IRequest
{
    public Guid UserId { get; set; }
}
