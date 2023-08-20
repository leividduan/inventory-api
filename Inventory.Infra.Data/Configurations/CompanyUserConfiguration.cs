using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Infra.Data.Configurations;

public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
	public void Configure(EntityTypeBuilder<CompanyUser> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Role)
			.HasConversion(new EnumToStringConverter<CompanyUser.Roles>());

		// Relationships

		builder.HasOne(x => x.Company)
			.WithMany(x => x.CompanyUser)
			.HasForeignKey(x => x.IdCompany);

		builder.HasOne(x => x.User)
			.WithMany(x => x.CompanyUser)
			.HasForeignKey(x => x.IdUser);
	}
}
