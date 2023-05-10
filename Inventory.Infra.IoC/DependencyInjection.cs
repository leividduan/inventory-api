using System.Reflection;
using FluentValidation;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data;
using Inventory.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infra.IoC;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		services.AddDbContextPool<AppDbContext>(options =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
		services.AddServices();
		services.AddRepositories();
		services.AddFluentValidations();

		return services;
	}

	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

		return services;
	}

	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

		return services;
	}

	public static IServiceCollection AddFluentValidations(this IServiceCollection services)
	{
		return services.AddValidatorsFromAssembly(Assembly.Load("Inventory.Domain"), ServiceLifetime.Transient);
	}
}
