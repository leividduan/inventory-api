using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Domain.Interfaces.Services;
using Inventory.Domain.Utils;

namespace Inventory.Domain.Services;

public class AuthService : IAuthService
{
  private readonly IUserRepository _repository;
  private readonly ITokenService _tokenService;
  public AuthService(IUserRepository repository, ITokenService tokenService)
  {
    _repository = repository;
    _tokenService = tokenService;
  }

  public async Task<(bool IsLogged, dynamic Data)> Login(User user)
  {
    var savedUser = await _repository.GetSingle(x => x.Email == user.Email);
    if (savedUser != null && PasswordUtils.VerifyPassword(user.Password, savedUser.Password))
    {
      var token = _tokenService.GenerateToken(savedUser);
      return (true, new
      {
        id = savedUser.Id,
        name = savedUser.Name,
        email = savedUser.Email,
        token
      });
    }

    return (false, null);
  }
}
