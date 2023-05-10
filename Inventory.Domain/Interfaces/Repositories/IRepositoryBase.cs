using System.Linq.Expressions;
using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Repositories;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
{
	public Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>> filter, string? include = null);
	public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, string? include = null);
	public Task<bool> Add(TEntity entity);
	public Task<bool> Edit(TEntity entity);
	public Task<bool> Delete(TEntity entity);
	public Task<int> Save();
}
