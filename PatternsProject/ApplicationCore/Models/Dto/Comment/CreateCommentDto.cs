namespace ApplicationCore.Models.Dto.Comment;

public class CreateCommentDto
{
	public string CommentText { get; set; }
	public Guid RequestId { get; set; }
	public Guid UserId { get; set; }
}
