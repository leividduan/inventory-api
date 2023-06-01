using System.Linq.Expressions;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;

namespace Inventory.Application.Services;

public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : Entity
{
	private readonly IRepositoryBase<TEntity> _repository;

	public ServiceBase(IRepositoryBase<TEntity> repository)
	{
		_repository = repository;
	}

	public void Dispose()
	{
		_repository?.Dispose();
	}

	public Task<bool> AddAsync(TEntity entity)
	{
		return _repository.AddAsync(entity);
	}

	public Task<bool> EditAsync(TEntity entity)
	{
		return _repository.EditAsync(entity);
	}

	public Task<bool> DeleteAsync(TEntity entity)
	{
		return _repository.DeleteAsync(entity);
	}

	public Task<int> SaveAsync()
	{
		return _repository.SaveAsync();
	}

	public Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, string? include = null)
	{
		return _repository.GetSingleAsync(filter, include);
	}

	public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string? include = null)
	{
		return _repository.GetAsync(filter, include);
	}
}
