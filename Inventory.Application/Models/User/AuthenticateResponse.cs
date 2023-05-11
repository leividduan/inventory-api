namespace Inventory.Application.Models.User;

public record AuthenticateResponse(
	int Id,
	string Name,
	string Email,
	string Token,
	string RefreshToken
);
