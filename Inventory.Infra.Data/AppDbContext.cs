using Microsoft.EntityFrameworkCore;

namespace Inventory.Infra.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
			         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
			property.SetColumnType("varchar(100)");

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

		//var cascadeFKs = modelBuilder.Model.GetEntityTypes()
		//		.SelectMany(t => t.GetForeignKeys())
		//		.Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade && fk.DeleteBehavior != DeleteBehavior.ClientCascade);

		//foreach (var fk in cascadeFKs)
		//	fk.DeleteBehavior = DeleteBehavior.Restrict;
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
