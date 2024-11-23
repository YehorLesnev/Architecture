using ApplicationCore.Models;

namespace ApplicationCore.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<UserModel>
{
	Task CreateAsync(UserModel entity, string password);
	Task CreateAsync(UserModel entity, string password, string role);
	Task CreateAllAsync(IEnumerable<UserModel> entities, string usersPassword);
	Task UpdateAsync(UserModel entity);
}