using Inventory.Domain.Entities.Validators;

namespace Inventory.Domain.Entities;

public class Company : Entity
{
	public string Name { get; set; }
	public string Document { get; set; }
	public bool IsActive { get; set; }

	public Company(string name, string document, bool isActive)
	{
		Name = name;
		Document = document;
		IsActive = isActive;
	}

	public override bool IsValid()
	{
		ValidationResult = new CompanyValidator().Validate(this);
		return ValidationResult.IsValid;
	}
}
