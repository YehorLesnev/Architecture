using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.Comment;

public class GetCommentsByRequestQuery : IRequest<IEnumerable<CommentModel>>
{
    public Guid RequestId { get; set; }
}
