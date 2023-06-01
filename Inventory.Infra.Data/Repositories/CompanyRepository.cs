using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;

namespace Inventory.Infra.Data.Repositories;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
	private readonly AppDbContext _context;

	public CompanyRepository(AppDbContext context) : base(context)
	{
		_context = context;
	}
}
