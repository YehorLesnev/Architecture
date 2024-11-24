using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.Request;

public class DeleteRequestCommand : IRequest
{
    public Guid Id { get; set; }
}