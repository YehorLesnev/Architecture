using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class UpdateRequestHandler(IRequestService service, IUserService userService) : IRequestHandler<UpdateRequestCommand, RequestModel>
{
    public async Task<RequestModel?> HandleAsync(UpdateRequestCommand request)
    {
        var requestModel = await service.GetAsync(r => r.Id == request.RequestId, asNoTracking: true);

        if (requestModel is null || await userService.GetAsync(u => u.Id == request.ManagerId) is null)
            return null;

        requestModel.Status = request.Status;
        requestModel.Type = request.Type;
        requestModel.DateFrom = request.DateFrom;
        requestModel.DateTo = request.DateTo;
        requestModel.ManagerId = request.ManagerId;

        await service.UpdateAsync(requestModel);

        return requestModel;
    }
}
