using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;

namespace ApplicationCore.Repositories.Implementations;

public class FileRepository(ApplicationDbContext dbContext)
		: BaseRepository<FileModel>(dbContext), IFileRepository
{
}