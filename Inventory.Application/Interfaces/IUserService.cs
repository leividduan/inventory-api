using Inventory.API.Models.User;
using Inventory.Application.Models.User;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IUserService : IServiceBase<User>
{
	Task<bool> ValidateAsync(User user);
	Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest request);
	Task<RegisterResponse> RegisterAsync(User user);
}
