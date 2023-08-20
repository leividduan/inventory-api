using Inventory.Domain.Entities.Validators;

namespace Inventory.Domain.Entities;

public class Company : Entity
{
	private readonly List<CompanyUser> _companyUsers;

	public Company(string name, string document, bool isActive)
	{
		Name = name;
		Document = document;
		IsActive = isActive;

		_companyUsers = new List<CompanyUser>();
	}

	public string Name { get; private set; }
	public string Document { get; private set; }
	public bool IsActive { get; private set; }

	// Relationship
	public ICollection<CompanyUser> CompanyUser => _companyUsers;

	public void AssociateUser(int idUser, CompanyUser.Roles role)
	{
		_companyUsers.Add(new CompanyUser(Id, idUser, role));
	}

	public void DisassociateUser(int idUser)
	{
		var companyUser = _companyUsers.FirstOrDefault(x => x.IdUser == idUser);
		if (companyUser == null)
			return;

		_companyUsers.Remove(companyUser);
	}

	public override bool IsValid()
	{
		ValidationResult = new CompanyValidator().Validate(this);
		return ValidationResult.IsValid;
	}
}
