using ApplicationCore.CQRS.Commands.File;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.File;

public class CreateFileHandler(IFileService fileService, IRequestService requestService, IMapper mapper) : IRequestHandler<CreateFileCommand>
{
	public async Task HandleAsync(CreateFileCommand request)
	{
		if(await requestService.GetAsync(x => x.Id == request.RequestId, asNoTracking: true) is null)
			throw new Exception("Request with such id not found");

		await fileService.CreateAsync(mapper.Map<FileModel>(request));
	}
}
