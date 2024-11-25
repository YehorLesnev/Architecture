using ApplicationCore.CQRS.Queries.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Implementations.Handlers.Request;
using ApplicationCore.Services.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace HRMS_Tests;

[TestFixture]
public class GetRequestsByUserHandlerTests
{
	private Mock<IRequestService> _requestServiceMock;
	private GetRequestsByUserHandler _getRequestsByUserHandler;

	[SetUp]
	public void SetUp()
	{
		_requestServiceMock = new Mock<IRequestService>();
		_getRequestsByUserHandler = new GetRequestsByUserHandler(_requestServiceMock.Object);
	}

	[Test]
	public async Task HandleAsync_WithValidUserId_ReturnsRequests()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var query = new GetRequestsByUserQuery { UserId = userId };
		var requests = new List<RequestModel>
				{
					new() { Id = Guid.NewGuid(), UserId = userId, Type = "Vacation" },
					new() { Id = Guid.NewGuid(), UserId = userId, Type = "Vacation" }
				};

		_requestServiceMock.Setup(s => s.GetAll(It.IsAny<Expression<Func<RequestModel, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()))
			.Returns(requests);

		// Act
		var result = await _getRequestsByUserHandler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.EqualTo(requests));
	}

	[Test]
	public async Task HandleAsync_WithInvalidUserId_ReturnsEmptyList()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var query = new GetRequestsByUserQuery { UserId = userId };

		_requestServiceMock.Setup(s => s.GetAll(It.IsAny<Expression<Func<RequestModel, bool>>>(), int.MaxValue, int.MaxValue, true)).Returns([]);

		// Act
		var result = await _getRequestsByUserHandler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.Empty);
	}
}