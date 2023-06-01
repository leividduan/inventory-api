namespace Inventory.Domain.Utils;

public static class DocumentUtils
{
	public static bool IsValidDocument(string documentToVerify)
	{
		if (documentToVerify.Length == 14)
			return ValidateCpf(documentToVerify);

		return ValidateCnpj(documentToVerify);
	}

	private static bool ValidateCnpj(string document)
	{
		// Remove any non-digit characters from the CNPJ
		var cleanCnpj = new string(document.Where(char.IsDigit).ToArray());

		// Check if CNPJ has a valid length
		if (cleanCnpj.Length != 14)
			return false;

		// Check if all digits are the same (e.g., 11111111111111)
		var allSameDigits = true;
		for (var i = 1; i < 14; i++)
			if (cleanCnpj[i] != cleanCnpj[0])
			{
				allSameDigits = false;
				break;
			}

		if (allSameDigits)
			return false;

		// Calculate the first verification digit
		var sum = 0;
		for (var i = 0; i < 12; i++)
			sum += (cleanCnpj[i] - '0') * (i < 4 ? 5 - i : 13 - i);
		var firstDigit = sum % 11 < 2 ? 0 : 11 - sum % 11;

		// Calculate the second verification digit
		sum = 0;
		for (var i = 0; i < 13; i++)
			sum += (cleanCnpj[i] - '0') * (i < 5 ? 6 - i : 14 - i);
		var secondDigit = sum % 11 < 2 ? 0 : 11 - sum % 11;

		// Check if the verification digits match
		return cleanCnpj[12] - '0' == firstDigit && cleanCnpj[13] - '0' == secondDigit;
	}


	private static bool ValidateCpf(string document)
	{
		// Remove any non-digit characters from the CPF
		var cleanCpf = new string(document.Where(char.IsDigit).ToArray());

		// Check if CPF has a valid length
		if (cleanCpf.Length != 11)
			return false;

		// Check if all digits are the same (e.g., 11111111111)
		var allSameDigits = true;
		for (var i = 1; i < 11; i++)
			if (cleanCpf[i] != cleanCpf[0])
			{
				allSameDigits = false;
				break;
			}

		if (allSameDigits)
			return false;

		// Calculate the first verification digit
		var sum = 0;
		for (var i = 0; i < 9; i++)
			sum += (cleanCpf[i] - '0') * (10 - i);
		var firstDigit = sum * 10 % 11;

		if (firstDigit == 10)
			firstDigit = 0;

		// Calculate the second verification digit
		sum = 0;
		for (var i = 0; i < 10; i++)
			sum += (cleanCpf[i] - '0') * (11 - i);
		var secondDigit = sum * 10 % 11;

		if (secondDigit == 10)
			secondDigit = 0;

		// Check if the verification digits match
		return cleanCpf[9] - '0' == firstDigit && cleanCpf[10] - '0' == secondDigit;
	}
}
