using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class DeleteRequestHandler(IRequestService service, IBalanceService balanceService, IMapper mapper) : IRequestHandler<DeleteRequestCommand>
{
    public async Task HandleAsync(DeleteRequestCommand request)
    {
        var requestToDelete = await service.GetAsync(r => r.Id == request.Id, asNoTracking: true);

        if (requestToDelete is null) 
            return;

		await balanceService.UpdateBalanceDaysOnRequestDelete(requestToDelete);

        await service.DeleteAsync(requestToDelete);
    }
}
