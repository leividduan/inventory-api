namespace Inventory.Domain.Entities
{
	public record Error(List<ErrorDetails> Errors);

	public record ErrorDetails(
		string Field,
		List<string> Messages
	);
}
