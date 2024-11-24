using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class CreateRequestHandler(IRequestService service, IUserService userService, IBalanceService balanceService, IMapper mapper) : IRequestHandler<CreateRequestCommand, RequestModel>
{
    public async Task<RequestModel?> HandleAsync(CreateRequestCommand request)
    {
        if (await userService.GetAsync(u => u.Id == request.UserId, asNoTracking: true) is null || await userService.GetAsync(u => u.Id == request.ManagerId, asNoTracking: true) is null)
            return null;

        var guid = Guid.NewGuid();
        var model = mapper.Map<RequestModel>(request);
        model.Id = guid;

        await balanceService.UpdateBalanceDaysOnRequestCreate(model);

        await service.CreateAsync(model);

        return await service.GetAsync(r => r.Id == guid, asNoTracking: true);
    }
}
