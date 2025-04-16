using ApplicationCore.Models;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Interfaces;

public interface IUserService : IBaseService<UserModel>
    {
        Task CreateAsync(UserModel entity, string password);

        Task CreateAllAsync(IEnumerable<UserModel> entities, string usersPassword);
    }
