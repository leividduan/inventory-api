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

		return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMiniMaxChars.IsMatch(password) &&
		       hasLowerChar.IsMatch(password) && hasSymbols.IsMatch(password);
	}

	public static string HashPassword(string password)
	{
		if (string.IsNullOrEmpty(password))
			return string.Empty;

		return BCrypt.Net.BCrypt.HashPassword(password, 12);
	}

	public static bool VerifyPassword(string passwordToVerify, string passwordVerified)
	{
		return BCrypt.Net.BCrypt.Verify(passwordToVerify, passwordVerified);
	}
}
