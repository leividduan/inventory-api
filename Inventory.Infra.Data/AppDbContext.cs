using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infra.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public DbSet<Company> Company { get; set; }
	public DbSet<User> User { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
			         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
			property.SetColumnType("varchar(100)");

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}

	public override int SaveChanges()
	{
		foreach (var entry in ChangeTracker.Entries()
			         .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("CreatedAt").CurrentValue = DateTime.Now;
				entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property("CreatedAt").IsModified = false;
				entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
			}
		}

		return base.SaveChanges();
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries()
			         .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("CreatedAt").CurrentValue = DateTime.Now;
				entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property("CreatedAt").IsModified = false;
				entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
