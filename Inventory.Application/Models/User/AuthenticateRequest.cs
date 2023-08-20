namespace Inventory.Application.Models.User;

public record AuthenticateRequest(
	string email,
	string password
);
