using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;

namespace Inventory.Application.Services;

public class CompanyService : ServiceBase<Company>, ICompanyService
{
	private readonly ICompanyRepository _repository;

	public CompanyService(ICompanyRepository repository) : base(repository)
	{
		_repository = repository;
	}

	public async Task<bool> AssociateUserAsync(int idCurrentUser, int idCompany, int idUserToAssociate,
		CompanyUser.Roles role)
	{
		var company =
			await _repository.GetSingleAsync(x => x.Id == idCompany && x.CompanyUser.Any(i => i.IdUser == idCurrentUser));
		if (company == null)
			return false;

		company.AssociateUser(idUserToAssociate, role);
		return await _repository.EditAsync(company);
	}

	public async Task<bool> DisassociateUserAsync(int idCurrentUser, int idCompany, int idUserToDisassociate)
	{
		var company =
			await _repository.GetSingleAsync(x => x.Id == idCompany && x.CompanyUser.Any(i => i.IdUser == idCurrentUser),
				"CompanyUser");
		if (company == null)
			return false;

		company.DisassociateUser(idUserToDisassociate);
		return await _repository.EditAsync(company);
	}
}
