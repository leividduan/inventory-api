using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Domain.Services;

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
		var key = Encoding.ASCII.GetBytes(_configuration["TokenJwt"] ?? string.Empty);

		var idsAllowedCompanies = user.CompanyUser.Any()
			? string.Join(",", user.CompanyUser.Select(s => s.IdCompany))
			: string.Empty;

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				new("IdUser", user.Id.ToString()),
				new(ClaimTypes.Name, user.Name),
				new(ClaimTypes.Email, user.Email),
				new("AllowedCompanies", idsAllowedCompanies)
			}),
			Expires = DateTime.UtcNow.AddDays(2),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
