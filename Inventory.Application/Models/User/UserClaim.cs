namespace Inventory.Application.Models.User;

public record UserClaim(int IdUser, string Name, string Email, List<int>? AllowedCompanies);
