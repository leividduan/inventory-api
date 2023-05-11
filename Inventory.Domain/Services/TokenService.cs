using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DezContas.Domain.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _configuration;
  public TokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public string GenerateToken(User user)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration["TokenJwt"] ?? "");

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[]
      {
          new Claim("UserId", user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Name),
          new Claim(ClaimTypes.Email, user.Email),
      }),
      Expires = DateTime.UtcNow.AddHours(2),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}
