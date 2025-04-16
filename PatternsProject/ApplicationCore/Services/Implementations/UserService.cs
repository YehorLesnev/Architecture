using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Implementations;

public class UserService(IUserRepository userRepository)
	: BaseService<UserModel>(userRepository), IUserService
{
	public override async Task CreateAsync(UserModel entity)
	{
		await userRepository.CreateAsync(entity);
	}

	public async Task CreateAsync(UserModel entity, string password)
	{
		await userRepository.CreateAsync(entity, password);
	}

	public async Task CreateAllAsync(IEnumerable<UserModel> entities, string usersPassword)
	{
		await userRepository.CreateAllAsync(entities, usersPassword);
	}

	public override async Task CreateAllAsync(IEnumerable<UserModel> entities)
	{
		foreach (var entity in entities)
		{
			await userRepository.CreateAsync(entity);
		}
	}

	public override async Task UpdateAsync(UserModel entity)
	{
		await userRepository.UpdateAsync(entity);
	}
}