using ApplicationCore.CQRS.Commands.Comment;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.Comment;

public class CreateCommentHandler(ICommentService service, IUserService userService, IRequestService requestService, IMapper mapper) : IRequestHandler<CreateCommentCommand>
{
	public async Task HandleAsync(CreateCommentCommand request)
	{
		if(await userService.GetAsync(u => u.Id == request.UserId, asNoTracking: true) is null || 
			await requestService.GetAsync(r => r.Id == request.RequestId, asNoTracking: true) is null)
				throw new Exception("Couldn't create request comment. User of request not found.");

		await service.CreateAsync(mapper.Map<CommentModel>(request));
	}
}
