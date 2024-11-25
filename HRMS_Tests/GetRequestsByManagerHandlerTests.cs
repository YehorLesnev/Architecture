using ApplicationCore.CQRS.Queries.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Implementations.Handlers.Request;
using ApplicationCore.Services.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace HRMS_Tests;

[TestFixture]
public class GetRequestsByManagerHandlerTests
{
	private Mock<IRequestService> _requestServiceMock;
	private GetRequestsByManagerHandler _handler;

	[SetUp]
	public void SetUp()
	{
		_requestServiceMock = new Mock<IRequestService>();
		_handler = new GetRequestsByManagerHandler(_requestServiceMock.Object);
	}

	[Test]
	public async Task HandleAsync_WithValidManagerId_ReturnsRequests()
	{
		// Arrange
		var managerId = Guid.NewGuid();
		var userId = Guid.NewGuid();
		var query = new GetRequestsByManagerQuery { ManagerId = managerId };
		var requests = new List<RequestModel>
				{
					new() { Id = Guid.NewGuid(), UserId = userId, ManagerId = managerId, Type = "Vacation" },
					new() { Id = Guid.NewGuid(), UserId = userId, ManagerId = managerId, Type = "Vacation" }
				};

		_requestServiceMock.Setup(s => s.GetAll(It.IsAny<Expression<Func<RequestModel, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()))
			.Returns(requests);

		// Act
		var result = await _handler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.EqualTo(requests));
	}

	[Test]
	public async Task HandleAsync_WithInvalidManagerId_ReturnsEmptyList()
	{
		// Arrange
		var managerId = Guid.NewGuid();
		var query = new GetRequestsByManagerQuery { ManagerId = managerId };

		_requestServiceMock.Setup(s => s.GetAll(It.IsAny<Expression<Func<RequestModel, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()))
			.Returns([]);

		// Act
		var result = await _handler.HandleAsync(query);

		// Assert
		Assert.That(result, Is.Empty);
	}
}