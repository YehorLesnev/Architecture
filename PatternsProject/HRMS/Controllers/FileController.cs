using ApplicationCore.CQRS.Commands.File;
using ApplicationCore.CQRS.Queries.File;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.File;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers;

[Route("files")]
[RequestSizeLimit(2_000_000_000)]
[RequestFormLimits(MultipartBodyLengthLimit = 2_000_000_000)]
public class FileController(IMediator mediator, IMapper mapper) : BaseController(mediator)
{
	[HttpGet("{requestId}")]
	public async Task<IActionResult> GetFilesByUser(Guid requestId)
	{
		var files = await Mediator.QueryListAsync<FileModel>(new GetFilesByRequestQuery { RequestId = requestId });

		return Ok(mapper.Map<IEnumerable<ResponseFileDto>>(files));
	}

	[HttpPost]
	public async Task<IActionResult> CreateFile([FromForm] CreateFileDto dto)
	{
		var command = mapper.Map<CreateFileCommand>(dto);

		await Mediator.SendAsync(command);

		return Ok();
	}
}
