using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.Request;

public class CreateRequestCommand : IRequest<RequestModel>
{
    public string Type { get; set; }
    public string Status { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public Guid ManagerId { get; set; }
}