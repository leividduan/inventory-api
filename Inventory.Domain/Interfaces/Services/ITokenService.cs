using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Services;

public interface ITokenService
{
	string GenerateToken(User user);
}
