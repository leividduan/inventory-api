namespace Inventory.Application.Models.User;

public record RegisterRequest(
	string Name,
	string Email,
	string Password
);
