using Inventory.Domain.Utils;

namespace Inventory.Domain.Tests.Utils;

public class PasswordUtilsTests
{
	[Trait("PasswordUtils", "IsValidPasswordStrength")]
	[Theory]
	[InlineData("")]
	[InlineData(null)]
	public void IsValidPasswordStrength_InvalidPassword_ReturnsFalse(string password)
	{
		// Arrange

		// Act
		var result = PasswordUtils.IsValidPasswordStrength(password);

		// Assert
		Assert.False(result);
	}

	[Trait("PasswordUtils", "IsValidPasswordStrength")]
	[Fact]
	public void IsValidPasswordStrength_WeakPassword_ReturnsFalse()
	{
		// Arrange
		var password = "weakpassword";

		// Act
		var result = PasswordUtils.IsValidPasswordStrength(password);

		// Assert
		Assert.False(result);
	}

	[Trait("PasswordUtils", "IsValidPasswordStrength")]
	[Fact]
	public void IsValidPasswordStrength_StrongPassword_ReturnsTrue()
	{
		// Arrange
		var password = "StrongPassword123!";

		// Act
		var result = PasswordUtils.IsValidPasswordStrength(password);

		// Assert
		Assert.True(result);
	}

	[Trait("PasswordUtils", "HashPassword")]
	[Fact]
	public void HashPassword_ValidPassword_ReturnsHashedPassword()
	{
		// Arrange
		var password = "mypassword";

		// Act
		var hashedPassword = PasswordUtils.HashPassword(password);

		// Assert
		Assert.NotNull(hashedPassword);
		Assert.NotEmpty(hashedPassword);
	}

	[Trait("PasswordUtils", "HashPassword")]
	[Fact]
	public void VerifyPassword_ValidPasswords_ReturnsTrue()
	{
		// Arrange
		var password = "mypassword";
		var hashedPassword = PasswordUtils.HashPassword(password);

		// Act
		var result = PasswordUtils.VerifyPassword(password, hashedPassword);

		// Assert
		Assert.True(result);
	}

	[Trait("PasswordUtils", "HashPassword")]
	[Fact]
	public void VerifyPassword_InvalidPasswords_ReturnsFalse()
	{
		// Arrange
		var password = "mypassword";
		var invalidPassword = "invalidpassword";
		var hashedPassword = PasswordUtils.HashPassword(password);

		// Act
		var result = PasswordUtils.VerifyPassword(invalidPassword, hashedPassword);

		// Assert
		Assert.False(result);
	}
}
