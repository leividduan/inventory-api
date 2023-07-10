namespace Inventory.Application.Models.Company;

public record CompanyPostRequest(
	string Name,
	string Document,
	bool IsActive
);
