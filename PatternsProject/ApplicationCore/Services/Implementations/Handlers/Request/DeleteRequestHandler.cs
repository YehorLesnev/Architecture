using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class DeleteRequestHandler(IRequestService service, IMapper mapper) : IRequestHandler<DeleteRequestCommand>
{
    public async Task HandleAsync(DeleteRequestCommand request)
    {
        if (await service.GetAsync(r => r.Id == request.Id, asNoTracking: true) is null)
            return;

        await service.DeleteAsync(mapper.Map<RequestModel>(request));
    }
}
