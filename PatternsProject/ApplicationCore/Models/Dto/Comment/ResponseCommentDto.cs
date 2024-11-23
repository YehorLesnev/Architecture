using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Dto.Comment;

public class ResponseCommentDto
{
	 public Guid CommentId { get; set; }
    public string CommentText { get; set; }
    public Guid RequestId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateTimeCreated { get; set; }
}
