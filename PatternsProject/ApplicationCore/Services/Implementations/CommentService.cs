using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Implementations;

public class CommentService(ICommentRepository repository)
		: BaseService<CommentModel>(repository), ICommentService
{
}