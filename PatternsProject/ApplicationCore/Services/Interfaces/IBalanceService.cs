using ApplicationCore.Models;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Interfaces;

public interface IBalanceService : IBaseService<BalanceModel>
{
	public Task UpdateBalanceDaysOnRequestUpdate(RequestModel request,  RequestModel oldRequest);

	public Task UpdateBalanceDaysOnRequestCreate(RequestModel request);

	public Task UpdateBalanceDaysOnRequestDelete(RequestModel request);
}