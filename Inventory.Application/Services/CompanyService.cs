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
}
