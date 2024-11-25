using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.Models;
using ApplicationCore.Services.Implementations.Handlers.Request;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Moq;
using System.Linq.Expressions;

namespace HRMS_Tests;

[TestFixture]
    public class CreateRequestHandlerTests
    {
        private Mock<IRequestService> _requestServiceMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IBalanceService> _balanceServiceMock;
        private Mock<IMapper> _mapperMock;

        private CreateRequestHandler _createRequestHandler;

        [SetUp]
        public void SetUp()
        {
            _requestServiceMock = new Mock<IRequestService>();
            _userServiceMock = new Mock<IUserService>();
            _balanceServiceMock = new Mock<IBalanceService>();
            _mapperMock = new Mock<IMapper>();

            _createRequestHandler = new CreateRequestHandler(
                _requestServiceMock.Object,
                _userServiceMock.Object,
                _balanceServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task HandleAsync_WithValidUserAndManager_ReturnsRequestModel()
        {
		    // Arrange
            var requestId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var managerId = Guid.NewGuid();
            
		    var request = new CreateRequestCommand
            {
                UserId = userId,
                ManagerId = managerId,
                Type = "Vacation",
			};

            var user = new UserModel { Id = userId };
            var manager = new UserModel { Id = managerId };
            var requestModel = new RequestModel { Id = Guid.NewGuid(), Type = "Vacation", UserId = userId };

            _userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
                .ReturnsAsync(user);
            _userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
                .ReturnsAsync(manager);
            _mapperMock.Setup(m => m.Map<RequestModel>(request))
                .Returns(requestModel);
            _requestServiceMock.Setup(s => s.CreateAsync(requestModel))
                .Returns(Task.CompletedTask);
            _requestServiceMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<RequestModel, bool>>>(), true))
                .ReturnsAsync(requestModel);

            // Act
            var result = await _createRequestHandler.HandleAsync(request);

            // Assert
            Assert.That(result, Is.EqualTo(requestModel));
        }

        [Test]
        public async Task HandleAsync_WithInvalidUser_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var managerId = Guid.NewGuid();

            var request = new CreateRequestCommand
            {
                UserId = userId,
                ManagerId = managerId,
                // Set other properties of the command as needed
            };

            var manager = new UserModel { Id = managerId };

            _userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
                .ReturnsAsync((UserModel)null);
            _userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
                .ReturnsAsync(manager);

            // Act
            var result = await _createRequestHandler.HandleAsync(request);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task HandleAsync_WithInvalidManager_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var managerId = Guid.NewGuid();

            var request = new CreateRequestCommand
            {
                UserId = userId,
                ManagerId = managerId,
                // Set other properties of the command as needed
            };

            var user = new UserModel { Id = userId };

            _userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
                .ReturnsAsync(user);
            _userServiceMock.Setup(u => u.GetAsync(It.IsAny<Expression<Func<UserModel, bool>>>(), true))
                .ReturnsAsync((UserModel)null);

            // Act
            var result = await _createRequestHandler.HandleAsync(request);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
