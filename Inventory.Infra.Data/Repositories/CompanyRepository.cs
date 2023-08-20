using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infra.Data.Repositories;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
	private readonly AppDbContext _context;

	public CompanyRepository(AppDbContext context) : base(context)
	{
		_context = context;
	}

	public List<Company> GetAllByIdUser(int _idUser)
	{
		return _context.Company.Include(x => x.CompanyUser).Where(c => c.CompanyUser.Any(i => i.IdUser == _idUser))
			.ToList();
	}
}
