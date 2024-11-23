﻿using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Implementations;

public class BalanceService(IBalanceRepository repository)
		: BaseService<BalanceModel>(repository), IBalanceService
{
}