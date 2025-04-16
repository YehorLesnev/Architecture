using ApplicationCore;
using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;

namespace ApplicationCore.Repositories.Implementations;

public class RequestRepository(ApplicationDbContext dbContext)
		: BaseRepository<RequestModel>(dbContext), IRequestRepository
{
}