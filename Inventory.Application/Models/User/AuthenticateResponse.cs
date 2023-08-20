namespace Inventory.Application.Models.User;

public record AuthenticateResponse(
	int id,
	string name,
	string email,
	string token,
	string refresh_token
);
