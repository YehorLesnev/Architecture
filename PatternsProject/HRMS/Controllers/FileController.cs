using ApplicationCore.CQRS.Commands.File;
using ApplicationCore.CQRS.Queries.File;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.File;
using ApplicationCore.Models.Dto.Request;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HRMS.Controllers;

[Route("files")]
[RequestSizeLimit(2_000_000_000)]
[RequestFormLimits(MultipartBodyLengthLimit = 2_000_000_000)]
public class FileController(IMediator mediator, IMapper mapper, IFileService fileService) : BaseController(mediator)
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

	[HttpPut]
	public async Task<IActionResult> UpdateFile(Guid fileId, [FromBody] UpdateFileDto dto)
	{
		var file = await fileService.GetAsync(x => x.FileId == fileId);

		if(file is null)
			return NotFound();

		using var memoryStream = new MemoryStream();

		dto.FileContent.CopyTo(memoryStream);

		file.FileContent = memoryStream.ToArray();

		await fileService.UpdateAsync(file);

		return Ok();
	}

	[HttpDelete]
	public async Task<IActionResult> UDeleteFile([FromQuery] Guid fileId)
	{
		var file = await fileService.GetAsync(x => x.FileId == fileId);

		if (file is null)
			return NotFound();

		await fileService.DeleteAsync(file);

		return Ok();
	}
}
