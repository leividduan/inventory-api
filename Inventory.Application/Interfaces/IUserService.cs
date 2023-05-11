using Inventory.API.Models.User;
using Inventory.Application.Models.User;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IUserService : IServiceBase<User>
{
	Task<bool> Validate(User user);
	Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
	Task<RegisterResponse> Register(User user);
}
