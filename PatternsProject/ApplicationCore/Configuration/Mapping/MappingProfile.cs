using ApplicationCore.Models.Dto.Balance;
using ApplicationCore.Models.Dto.Comment;
using ApplicationCore.Models.Dto.File;
using ApplicationCore.Models.Dto.Notification;
using ApplicationCore.Models.Dto.Request;
using ApplicationCore.Models;
using AutoMapper;
using ApplicationCore.Models.Dto.User;
using ApplicationCore.Identity;
using Microsoft.AspNetCore.Http;
using ApplicationCore.CQRS.Commands.Request;

namespace ApplicationCore.Configuration.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// Request Mappings
		CreateMap<RequestModel, ResponseRequestDto>();

		CreateMap<CreateRequestDto, RequestModel>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.DateCreated, opt => opt.Ignore());
		CreateMap<CreateRequestDto, CreateRequestCommand>();
		CreateMap<CreateRequestCommand, RequestModel>();

		CreateMap<UpdateRequestDto, RequestModel>();
		CreateMap<UpdateRequestDto, UpdateRequestCommand>();

		CreateMap<DeleteRequestCommand, RequestModel>();

		// User Mappings
		CreateMap<UserModel, ResponseUserDto>();

		CreateMap<RequestUserDto, UserModel>()
			.ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
			.AfterMap((src, dest) => dest.ProfilePicture = (src.ProfilePicture is null)
						? string.Empty
						: Convert.ToBase64String(GetProfilePicture(src.ProfilePicture)));

		// Comment Mappings
		CreateMap<CommentModel, ResponseCommentDto>();
		CreateMap<CreateCommentDto, CommentModel>()
			.ForMember(dest => dest.CommentId, opt => opt.Ignore())
			.ForMember(dest => dest.DateTimeCreated, opt => opt.Ignore());

		// File Mappings
		CreateMap<FileModel, ResponseFileDto>();
		CreateMap<CreateFileDto, FileModel>()
			.ForMember(dest => dest.FileId, opt => opt.Ignore());

		// Notification Mappings
		CreateMap<NotificationModel, ResponseNotificationDto>();
		CreateMap<CreateNotificationDto, NotificationModel>()
			.ForMember(dest => dest.NotificationId, opt => opt.Ignore())
			.ForMember(dest => dest.DateCreated, opt => opt.Ignore());

		// Balance Mappings
		CreateMap<BalanceModel, ResponseBalanceDto>();
		CreateMap<UpdateBalanceDto, BalanceModel>()
			.ForMember(dest => dest.BalanceId, opt => opt.Ignore());
	}

	private static byte[] GetProfilePicture(IFormFile profilePicture)
	{
		using var memoryStream = new MemoryStream();

		profilePicture.CopyTo(memoryStream);

		return memoryStream.ToArray();
	}
}
