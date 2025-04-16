using ApplicationCore.Constants;
using ApplicationCore.CQRS.Queries.Balance;
using ApplicationCore.Models;
using ApplicationCore.Services.Implementations.Handlers.Balance;
using ApplicationCore.Services.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace HRMS_Tests;

[TestFixture]
public class GetBalancesByUserHandlerTests
{
	private Mock<IBalanceService> _balanceServiceMock;
	private GetBalancesByUserHandler _handler;

	[SetUp]
	public void SetUp()
	{
		_balanceServiceMock = new Mock<IBalanceService>();
		_handler = new GetBalancesByUserHandler(_balanceServiceMock.Object);
	}

	[Test]
	public async Task HandleAsync_WithValidUserId_ReturnsBalances()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var query = new GetBalancesByUserQuery { UserId = userId };
		var balances = new List<BalanceModel>
				{
					new BalanceModel { UserId = userId, BalanceAmount = 100.0m, Type = Constants.BalanceTypeNames.Vacation },
					new BalanceModel { UserId = userId, BalanceAmount = 200.0m, Type = Constants.BalanceTypeNames.SickLeave}
				};

		_balanceServiceMock.Setup(s => s.GetAll(It.IsAny<Expression<Func<BalanceModel, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()))
			.Returns(balances);

		// Act
		var result = await _handler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.EqualTo(balances));
	}

	[Test]
	public async Task HandleAsync_WithInvalidUserId_ReturnsEmptyList()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var query = new GetBalancesByUserQuery { UserId = userId };

		_balanceServiceMock.Setup(s => s.GetAll(It.IsAny<Expression<Func<BalanceModel, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()))
			.Returns([]);

		// Act
		var result = await _handler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.Empty);
	}
}