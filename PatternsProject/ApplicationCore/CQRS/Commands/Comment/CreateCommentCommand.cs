using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.Comment;

public class CreateCommentCommand : IRequest
{
    public string CommentText { get; set; }
    public Guid RequestId { get; set; }
    public Guid UserId { get; set; }
}
