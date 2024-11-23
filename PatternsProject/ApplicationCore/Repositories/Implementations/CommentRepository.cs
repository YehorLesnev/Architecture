using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;

namespace ApplicationCore.Repositories.Implementations;

public class CommentRepository(ApplicationDbContext dbContext)
	: BaseRepository<CommentModel>(dbContext), ICommentRepository
{
}