using ApplicationCore.CQRS.Queries.User;
using ApplicationCore.Models;
using ApplicationCore.Services.Implementations.Handlers.User;
using ApplicationCore.Services.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace HRMS_Tests;

public class GetUserByIdHandlerTests
{
	private Mock<IUserService> _userServiceMock;
	private GetUserByIdHandler _handler;

	[SetUp]
	public void SetUp()
	{
		_userServiceMock = new Mock<IUserService>();
		_handler = new GetUserByIdHandler(_userServiceMock.Object);
	}

	[Test]
	public async Task HandleAsync_WithValidUserId_ReturnsUserModel()
	{
		// Arrange
		var userGuid = Guid.NewGuid();
		var query = new GetUserByIdQuery { UserId = userGuid };
		var expectedUserModel = new UserModel { Id = userGuid, FullName = "John Doe" };

		_userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
			.ReturnsAsync(expectedUserModel);

		// Act
		var result = await _handler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.EqualTo(expectedUserModel));
	}

	[Test]
	public async Task HandleAsync_WithInvalidUserId_ReturnsNull()
	{
		// Arrange
		var query = new GetUserByIdQuery { UserId = Guid.NewGuid() };

		_userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
			.ReturnsAsync((UserModel)null);

		// Act
		var result = await _handler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.Null);
	}
}