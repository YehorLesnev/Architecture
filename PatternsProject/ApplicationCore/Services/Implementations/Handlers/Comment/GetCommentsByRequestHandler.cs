using ApplicationCore.CQRS.Queries.Comment;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Comment;

public class GetCommentsByRequestHandler(ICommentService service) : IRequestHandler<GetCommentsByRequestQuery, IEnumerable<CommentModel>>
{
    public async Task<IEnumerable<CommentModel>> HandleAsync(GetCommentsByRequestQuery query)
    {
        return service.GetAll(u => u.RequestId == query.RequestId, asNoTracking: true);
    }
}