using Inventory.Domain.Entities.Validators;
using Inventory.Domain.Utils;

namespace Inventory.Domain.Entities;

public class User : Entity
{
	public User(string name, string email, string password)
	{
		Name = name;
		Email = email;
		Password = password;
		IsActive = true;
	}

	public User(string name, string email, string password, bool isActive)
	{
		Name = name;
		Email = email;
		Password = password;
		IsActive = isActive;
	}

	public string Name { get; private set; }
	public string Email { get; private set; }
	public string Password { get; private set; }
	public bool IsActive { get; private set; }

	public void HashPassword()
	{
		Password = PasswordUtils.HashPassword(Password);
	}

	public override bool IsValid()
	{
		ValidationResult = new UserValidator().Validate(this);
		return ValidationResult.IsValid;
	}
}
