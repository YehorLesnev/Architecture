using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.File;

public class GetFilesByRequestQuery : IRequest<IEnumerable<FileModel>>
{
	public Guid RequestId { get; set; }
}
