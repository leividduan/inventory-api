using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Services;

public interface IAuthService
{
  public Task<(bool IsLogged, dynamic Data)> Login(User user);
}
