using ApplicationCore.Models;
using ApplicationCore.Observer.Interfaces;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Observer.Implementations;

public class InAppObserver : IObserver
{
    private readonly INotificationService _notificationService;

	private readonly IMapper _mapper;

	public InAppObserver(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
		_mapper = mapper;
	}

    public async Task UpdateAsync(INotification notification)
    {
        var notificationModel = _mapper.Map<NotificationModel>(notification);

		await _notificationService.CreateAsync(notificationModel);
	}
}
