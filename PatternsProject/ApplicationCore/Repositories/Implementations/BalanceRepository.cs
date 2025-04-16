using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;

namespace ApplicationCore.Repositories.Implementations;

public class BalanceRepository(ApplicationDbContext dbContext)
	: BaseRepository<BalanceModel>(dbContext), IBalanceRepository
{
}