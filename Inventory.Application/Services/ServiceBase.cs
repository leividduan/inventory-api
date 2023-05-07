using System.Linq.Expressions;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;

namespace Inventory.Application.Services
{
	public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : Entity
	{
		private readonly IRepositoryBase<TEntity> _repository;

		public ServiceBase(IRepositoryBase<TEntity> repository)
		{
			_repository = repository;
		}

		public Task<bool> Add(TEntity entity)
		{
			return _repository.Add(entity);
		}

		public Task<bool> Edit(TEntity entity)
		{
			return _repository.Edit(entity);
		}

		public Task<bool> Delete(TEntity entity)
		{
			return _repository.Delete(entity);
		}

		public Task<int> Save()
		{
			return _repository.Save();
		}

		public Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>> filter, string? include = null)
		{
			return _repository.GetSingle(filter, include);
		}

		public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, string? include = null)
		{
			return _repository.Get(filter, include);
		}

		public void Dispose()
		{
			_repository?.Dispose();
		}
	}
}
