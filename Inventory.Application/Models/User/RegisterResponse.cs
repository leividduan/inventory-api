namespace Inventory.API.Models.User;

public record RegisterResponse(
	int id,
	string name,
	string email,
	bool is_active,
	string token
);
