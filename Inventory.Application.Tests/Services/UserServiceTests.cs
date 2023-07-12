using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Domain.Interfaces.Services;

namespace Inventory.Application.Tests.Services;

public class UserServiceTests
{
	private readonly Mock<IUserRepository> _mockRepository;
	private readonly Mock<ITokenService> _mockTokenService;
	private readonly UserService _service;

	public UserServiceTests()
	{
		_mockRepository = new Mock<IUserRepository>();
		_mockTokenService = new Mock<ITokenService>();
		_service = new UserService(_mockRepository.Object, _mockTokenService.Object);
	}

	[Trait("UserService", "ValidateAsync")]
	[Fact]
	public async Task UserService_ValidateAsync_Should_Be_True()
	{
		// Arrange
		var userExpected = new User("Deivid", "deivid@mail.com", "Teste#123", true);

		// Act
		var result = await _service.ValidateAsync(userExpected);

		// Assert
		Assert.True(result);
		Assert.Null(userExpected.GetErrors());
	}

	[Trait("UserService", "ValidateAsync")]
	[Fact]
	public async Task UserService_ValidateAsync_Should_Be_False()
	{
		// Arrange
		var userExpected = new User("Deivid", "deivid@mail.com", "Teste#123", true);
		_mockRepository.Setup(repo => repo.GetSingleAsync(x => x.Email == userExpected.Email, null))
			.ReturnsAsync(userExpected);

		// Act
		var result = await _service.ValidateAsync(userExpected);

		// Assert
		Assert.False(result);
		Assert.NotNull(userExpected.GetErrors());
	}
}
