namespace Inventory.Application.Models.Company;

public record CompanyResponse(
	int Id,
	string Name,
	string Document,
	bool IsActive,
	DateTime CreatedAt,
	DateTime UpdatedAt
);
