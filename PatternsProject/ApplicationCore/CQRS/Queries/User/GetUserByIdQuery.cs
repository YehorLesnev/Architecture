using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.User;

public class GetUserByIdQuery : IRequest<UserModel>
{
    public Guid UserId { get; set; }
}