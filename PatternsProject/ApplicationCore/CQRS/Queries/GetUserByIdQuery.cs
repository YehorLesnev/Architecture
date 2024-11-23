using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries;

public class GetUserByIdQuery : IRequest<UserModel>
{
    public Guid UserId { get; set; }
}