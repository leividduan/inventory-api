using System.Linq.Expressions;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infra.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
{
	private readonly AppDbContext _context;

	public RepositoryBase(AppDbContext context)
	{
		_context = context;
	}

	public async Task<bool> AddAsync(TEntity entity)
	{
		await _context.Set<TEntity>().AddAsync(entity);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> EditAsync(TEntity entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		_context.Set<TEntity>().Update(entity);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteAsync(TEntity entity)
	{
		_context.Set<TEntity>().Remove(entity);
		await _context.SaveChangesAsync();
		return await _context.SaveChangesAsync() > 0;
	}

	public Task<int> SaveAsync()
	{
		return _context.SaveChangesAsync();
	}

	public void Dispose()
	{
		_context?.Dispose();
	}

	public Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, string? include = null)
	{
		if (string.IsNullOrEmpty(include))
			return _context.Set<TEntity>().SingleOrDefaultAsync(filter);
		return _context.Set<TEntity>().Include(include).SingleOrDefaultAsync(filter);
	}

	public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string? include = null)
	{
		var entities = _context.Set<TEntity>().AsNoTracking();

		if (!string.IsNullOrEmpty(include))
			entities = entities.Include(include);
		if (filter != null)
			entities = entities.Where(filter);

		return entities.ToListAsync();
	}
}
