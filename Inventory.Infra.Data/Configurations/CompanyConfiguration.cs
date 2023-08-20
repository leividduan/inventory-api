using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infra.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		builder.HasKey(x => x.Id);

		// Properties
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(x => x.Document)
			.IsRequired()
			.HasMaxLength(18);

		// Indexes
		builder.HasIndex(i => i.Document)
			.IsUnique();

		// Relationships
	}
}
