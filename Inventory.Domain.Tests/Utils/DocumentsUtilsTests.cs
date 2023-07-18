using Inventory.Domain.Utils;

namespace Inventory.Domain.Tests.Utils;

public class DocumentUtilsTests
{
	[Trait("DocumentUtils", "IsValidDocument")]
	[Theory]
	[InlineData("12345678901234")] // Invalid length
	[InlineData("11111111111111")] // All same digits
	[InlineData("1234567890123A")] // Non-digit characters
	public void IsValidDocument_InvalidCnpj_ReturnsFalse(string document)
	{
		// Arrange

		// Act
		var result = DocumentUtils.IsValidDocument(document);

		// Assert
		Assert.False(result);
	}

	[Trait("DocumentUtils", "IsValidDocument")]
	[Fact]
	public void IsValidDocument_ValidCnpj_ReturnsTrue()
	{
		// Arrange
		var cnpj = "29.226.919/0001-80";

		// Act
		var result = DocumentUtils.IsValidDocument(cnpj);

		// Assert
		Assert.True(result);
	}

	[Trait("DocumentUtils", "IsValidDocument")]
	[Theory]
	[InlineData("12345678901")] // Invalid length
	[InlineData("11111111111")] // All same digits
	[InlineData("1234567890A")] // Non-digit characters
	public void IsValidDocument_InvalidCpf_ReturnsFalse(string document)
	{
		// Arrange

		// Act
		var result = DocumentUtils.IsValidDocument(document);

		// Assert
		Assert.False(result);
	}

	[Trait("DocumentUtils", "IsValidDocument")]
	[Fact]
	public void IsValidDocument_ValidCpf_ReturnsTrue()
	{
		// Arrange
		var cpf = "123.456.789-09";

		// Act
		var result = DocumentUtils.IsValidDocument(cpf);

		// Assert
		Assert.True(result);
	}
}
