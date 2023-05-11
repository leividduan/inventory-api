namespace Inventory.API.Models.User;

public record RegisterResponse(
	int Id,
	string Name,
	string Email,
	bool IsActive,
	DateTime CreatedAt,
	DateTime UpdatedAt
);
