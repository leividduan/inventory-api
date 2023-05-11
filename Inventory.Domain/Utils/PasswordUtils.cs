using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Inventory.Domain.Utils;

public static class PasswordUtils
{
  public static bool IsValidPasswordStrength(string password)
  {
    if (string.IsNullOrEmpty(password)) return false;

    var hasNumber = new Regex(@"[0-9]+");
    var hasUpperChar = new Regex(@"[A-Z]+");
    var hasMiniMaxChars = new Regex(@".{8,30}");
    var hasLowerChar = new Regex(@"[a-z]+");
    var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

    return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMiniMaxChars.IsMatch(password) && hasLowerChar.IsMatch(password) && hasSymbols.IsMatch(password);
  }

  public static string HashPassword(string password)
  {
    if (string.IsNullOrEmpty(password))
      return string.Empty;

    var input = Encoding.UTF8.GetBytes(password);
    using (var hashAlgorithm = HashAlgorithm.Create("sha256"))
    {
      if (hashAlgorithm == null) return string.Empty;

      return Convert.ToBase64String(hashAlgorithm.ComputeHash(input));
    }
  }

  public static bool VerifyPassword(string passwordToVerify, string passwordVerified)
  {
    var hashedPasswordToVerify = HashPassword(passwordToVerify);
    return hashedPasswordToVerify.Equals(passwordVerified);
  }
}
