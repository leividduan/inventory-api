using System.Security.Claims;
using Inventory.Application.Models.User;

namespace Inventory.API.Helpers;

public static class ClaimHelper
{
	public static UserClaim? GetUserData(this ClaimsPrincipal principal)
	{
		if (principal.Claims == null || !principal.Claims.Any())
			return null;

		List<int>? idsAllowedCompanies = null;

		var idUser = int.Parse(principal.Claims.First(x => x.Type == "IdUser").Value);
		var name = principal.Claims.First(x => x.Type == ClaimTypes.Name).Value;
		var email = principal.Claims.First(x => x.Type == ClaimTypes.Email).Value;
		var allowedCompanies = principal.Claims.FirstOrDefault(x => x.Type == "AllowedCompanies")?.Value;
		if (!string.IsNullOrEmpty(allowedCompanies))
			idsAllowedCompanies = allowedCompanies.Split(",").Select(n => Convert.ToInt32(n)).ToList();

		return new UserClaim(idUser, name, email, idsAllowedCompanies);
	}

	public static int GetUserIdClaim(this IEnumerable<Claim> claims)
	{
		return int.Parse(claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "0");
	}
}
