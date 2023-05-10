using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;

namespace Inventory.Infra.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity :  Entity
{
	private readonly AppDbContext _context;

	public RepositoryBase(AppDbContext context)
	{
		_context = context;
	}

	public async Task<bool> Add(TEntity entity)
	{
		await _context.Set<TEntity>().AddAsync(entity);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Edit(TEntity entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		_context.Set<TEntity>().Update(entity);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Delete(TEntity entity)
	{
		_context.Set<TEntity>().Remove(entity);
		await _context.SaveChangesAsync();
		return await _context.SaveChangesAsync() > 0;
	}

	public Task<int> Save()
	{
		return _context.SaveChangesAsync();
	}

	public Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>> filter, string? include = null)
	{
		if (string.IsNullOrEmpty(include))
			return _context.Set<TEntity>().SingleOrDefaultAsync(filter);
		else
			return _context.Set<TEntity>().Include(include).SingleOrDefaultAsync(filter);
	}

	public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, string? include = null)
	{
		var entities = _context.Set<TEntity>().AsNoTracking();

		if (!String.IsNullOrEmpty(include))
			entities = entities.Include(include);
		if (filter != null)
			entities = entities.Where(filter);

		return entities.ToListAsync();
	}

	public void Dispose()
	{
		_context?.Dispose();
	}
}
