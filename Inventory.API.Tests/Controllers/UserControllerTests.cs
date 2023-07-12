using Inventory.API.Models.User;
using Inventory.Application.Interfaces;
using Inventory.Application.Models.User;
using Inventory.Domain.Entities;

namespace Inventory.API.Tests.Controllers;

public class UserControllerTests
{
	private readonly UserController _controller;
	private readonly Mock<IUserService> _mockService;

	public UserControllerTests()
	{
		_mockService = new Mock<IUserService>();
		_controller = new UserController(_mockService.Object);
	}

	[Trait("User", "Register")]
	[Fact]
	public async Task User_Register_Successfully_OkObjectResult()
	{
		// Arrange
		_mockService.Setup(service => service.Validate(It.IsAny<User>())).ReturnsAsync(true);
		_mockService.Setup(service => service.RegisterAsync(It.IsAny<User>()))
			.ReturnsAsync(new RegisterResponse(1, "Deivid", "deivid@mail.com", true, DateTime.Now, DateTime.Now));
		var request = new RegisterRequest("Deivid", "deivid@mail.com", "Teste#123");

		// Act
		var result = await _controller.Register(request);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.IsAssignableFrom<RegisterResponse>(okResult.Value);
		Assert.Equal(200, okResult.StatusCode);
	}

	[Trait("User", "Register")]
	[Fact]
	public async Task User_Register_Invalid_User_BadRequestObjectResult()
	{
		// Arrange
		_mockService.Setup(service => service.Validate(It.IsAny<User>())).ReturnsAsync(false);
		var request = new RegisterRequest("Deivid", "deivid@mail.com", "teste");

		// Act
		var result = await _controller.Register(request);

		// Assert
		var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.IsAssignableFrom<Error>(badRequestObjectResult.Value);
		Assert.Equal(400, badRequestObjectResult.StatusCode);
	}

	[Trait("User", "Register")]
	[Fact]
	public async Task User_Register_Invalid_Database_User_BadRequestObjectResult()
	{
		// Arrange
		_mockService.Setup(service => service.Validate(It.IsAny<User>())).ReturnsAsync(true);
		var request = new RegisterRequest("Deivid", "deivid@mail.com", "teste");

		// Act
		var result = await _controller.Register(request);

		// Assert
		var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.IsAssignableFrom<Error>(badRequestObjectResult.Value);
		Assert.Equal(400, badRequestObjectResult.StatusCode);
	}

	[Trait("User", "Authenticate")]
	[Fact]
	public async Task User_Authenticate_Successfully_OkObjectResult()
	{
		// Arrange
		_mockService.Setup(service => service.AuthenticateAsync(It.IsAny<AuthenticateRequest>()))
			.ReturnsAsync(new AuthenticateResponse(1, "Deivid", "deivid@mail.com", "NEW_TOKEN", "NEW_REFRESH_TOKEN"));
		var request = new AuthenticateRequest("deivid@mail.com", "Teste#123");

		// Act
		var result = await _controller.Authenticate(request);

		// Assert
		var okObjectResult = Assert.IsType<OkObjectResult>(result);
		Assert.IsAssignableFrom<dynamic>(okObjectResult.Value);
		Assert.Equal(200, okObjectResult.StatusCode);
	}

	[Trait("User", "Authenticate")]
	[Fact]
	public async Task User_Authenticate_Failed_NotFoundObjectResult()
	{
		// Arrange
		var request = new AuthenticateRequest("deivid@mail.com", "Teste#123");

		// Act
		var result = await _controller.Authenticate(request);

		// Assert
		var okObjectResult = Assert.IsType<NotFoundObjectResult>(result);
		Assert.IsAssignableFrom<dynamic>(okObjectResult.Value);
		Assert.Equal(404, okObjectResult.StatusCode);
	}
}
