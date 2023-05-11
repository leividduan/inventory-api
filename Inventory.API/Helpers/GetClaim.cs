using System.Security.Claims;

namespace Inventory.API.Helpers;

public static class GetClaim
{
  public static int GetUserIdClaim(this IEnumerable<Claim> claims)
  {
    return int.Parse(claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "0");
  }
}
