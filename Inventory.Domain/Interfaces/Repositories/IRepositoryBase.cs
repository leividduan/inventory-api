using System.Linq.Expressions;
using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Repositories;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
{
	public Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, string? include = null);
	public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string? include = null);
	public Task<bool> AddAsync(TEntity entity);
	public Task<bool> EditAsync(TEntity entity);
	public Task<bool> DeleteAsync(TEntity entity);
	public Task<int> SaveAsync();
}
