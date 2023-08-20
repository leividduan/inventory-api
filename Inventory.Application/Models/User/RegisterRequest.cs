namespace Inventory.Application.Models.User;

public record RegisterRequest(
	string name,
	string email,
	string password
);
