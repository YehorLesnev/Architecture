﻿using ApplicationCore.CQRS.Commands.Notification;
using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.Models;
using ApplicationCore.Observer.Implementations;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.Request;

public class UpdateRequestHandler(IRequestService service, IUserService userService, IBalanceService balanceService, IMapper mapper, NotificationObserverService notificationObserverService) : IRequestHandler<UpdateRequestCommand, RequestModel>
{
	public async Task<RequestModel?> HandleAsync(UpdateRequestCommand request)
	{
		var requestModel = await service.GetAsync(r => r.Id == request.RequestId, asNoTracking: true);

		if (requestModel is null || await userService.GetAsync(u => u.Id == request.ManagerId, asNoTracking: true) is null)
			return null;
		
		var oldrequset = requestModel;

		requestModel.Status = request.Status;
		requestModel.IsApproved = request.IsApproved;
		requestModel.Type = request.Type;
		requestModel.DateFrom = request.DateFrom;
		requestModel.DateTo = request.DateTo;
		requestModel.ManagerId = request.ManagerId;

		await balanceService.UpdateBalanceDaysOnRequestUpdate(requestModel, oldrequset);

		await service.UpdateAsync(requestModel);

		if (requestModel.IsApproved)
		{
			await notificationObserverService.NotifyAllAsync(new CreateNotificationCommand
			{
				UserId = requestModel.UserId,
				SenderId = requestModel.ManagerId,
				Text = $"Request approved{(string.IsNullOrEmpty(request.NotificationText) ? string.Empty : $": {request.NotificationText}")}"
			});
		}
		else
		{
			await notificationObserverService.NotifyAllAsync(new CreateNotificationCommand
			{
				UserId = requestModel.UserId,
				SenderId = requestModel.ManagerId,
				Text = $"Request was not approved{(string.IsNullOrEmpty(request.NotificationText) ? string.Empty : $": {request.NotificationText}")}"
			});
		}

		return requestModel;
	}
}
