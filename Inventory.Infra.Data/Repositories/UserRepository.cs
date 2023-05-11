using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;

namespace Inventory.Infra.Data.Repositories
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		private readonly AppDbContext _context;
		public UserRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
