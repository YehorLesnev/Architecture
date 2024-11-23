using ApplicationCore.CQRS.Queries.File;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.File;

public class GetFilesByRequestHandler(IFileService service) : IRequestHandler<GetFilesByRequestQuery, IEnumerable<FileModel>>
{
	public async Task<IEnumerable<FileModel>?> HandleAsync(GetFilesByRequestQuery request)
	{
		return service.GetAll(f => f.RequestId == request.RequestId);
	}
}
