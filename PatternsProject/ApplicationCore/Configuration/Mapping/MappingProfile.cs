using ApplicationCore.Models.Dto.Balance;
using ApplicationCore.Models.Dto.Comment;
using ApplicationCore.Models.Dto.File;
using ApplicationCore.Models.Dto.Notification;
using ApplicationCore.Models.Dto.Request;
using ApplicationCore.Models;
using AutoMapper;
using ApplicationCore.Models.Dto.User;
using Microsoft.AspNetCore.Http;
using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.CQRS.Commands.Comment;
using ApplicationCore.CQRS.Commands.File;
using ApplicationCore.CQRS.Commands.Notification;

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
						: Convert.ToBase64String(GetFormFileBytes(src.ProfilePicture)));

		// Comment Mappings
		CreateMap<CommentModel, ResponseCommentDto>();
		CreateMap<CreateCommentDto, CommentModel>()
			.ForMember(dest => dest.CommentId, opt => opt.Ignore())
			.ForMember(dest => dest.DateTimeCreated, opt => opt.Ignore());

		CreateMap<CreateCommentDto, CreateCommentCommand>();
		CreateMap<CreateCommentCommand, CommentModel>();
		
		// File Mappings
		CreateMap<FileModel, ResponseFileDto>()
			.ForMember(dest => dest.FileContent, opt => opt.Ignore())
			.AfterMap((src, dest) => dest.FileContent = src.FileContent is null 
				? string.Empty
				: Convert.ToBase64String(src.FileContent));
		CreateMap<CreateFileCommand, FileModel>();
		CreateMap<CreateFileDto, FileModel>()
			.ForMember(dest => dest.FileContent, opt => opt.Ignore())
			.AfterMap((src, dest) => dest.FileContent = src.FileContent is null 
				? []
				: GetFormFileBytes(src.FileContent));
		CreateMap<CreateFileDto, CreateFileCommand>()
			.ForMember(dest => dest.FileContent, opt => opt.Ignore())
			.AfterMap((src, dest) => dest.FileContent = src.FileContent is null 
				? []
				: GetFormFileBytes(src.FileContent));

		// Notification Mappings
		CreateMap<NotificationModel, ResponseNotificationDto>();
		CreateMap<CreateNotificationCommand, NotificationModel>();
		CreateMap<CreateNotificationDto, CreateNotificationCommand>();
		CreateMap<CreateNotificationDto, NotificationModel>();

		// Balance Mappings
		CreateMap<BalanceModel, ResponseBalanceDto>();
		CreateMap<UpdateBalanceDto, BalanceModel>()
			.ForMember(dest => dest.BalanceId, opt => opt.Ignore());
	}

	private static byte[] GetFormFileBytes(IFormFile file)
	{
		using var memoryStream = new MemoryStream();

		file.CopyTo(memoryStream);

		return memoryStream.ToArray();
	}
}
