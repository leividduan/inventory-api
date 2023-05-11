namespace Inventory.Application.Models.User;

public record AuthenticateRequest(
	string Email,
	string Password
);
