namespace Inventory.Domain.Entities;

public class CompanyUser
{
	[Flags]
	public enum Roles
	{
		Standard = 0,
		Manager = 1,
		Admin = 2
	}

	public CompanyUser(int id, int idCompany, int idUser, Roles role)
	{
		Id = id;
		IdCompany = idCompany;
		IdUser = idUser;
		Role = role;
	}

	public CompanyUser(int idCompany, int idUser, Roles role)
	{
		IdCompany = idCompany;
		IdUser = idUser;
		Role = role;
	}

	public int Id { get; private set; }
	public int IdCompany { get; private set; }
	public int IdUser { get; private set; }
	public Roles Role { get; }

	// Relationships

	public Company Company { get; }
	public User User { get; }
}
