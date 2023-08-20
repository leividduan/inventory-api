using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface ICompanyService : IServiceBase<Company>
{
	Task<bool> AssociateUserAsync(int idCurrentUser, int idCompany, int idUserToAssociate, CompanyUser.Roles role);
	Task<bool> DisassociateUserAsync(int idCurrentUser, int idCompany, int idUserToDisassociate);
}
