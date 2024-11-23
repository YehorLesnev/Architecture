using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.CQRS.Commands.Request;

public class UpdateRequestCommand : IRequest<RequestModel>
{
	public Guid RequestId { get; set; }
    public string Type { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string Status { get; set; }
    public Guid ManagerId { get; set; }
}
