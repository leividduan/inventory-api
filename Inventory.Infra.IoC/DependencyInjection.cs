using System.Reflection;
using System.Text;
using FluentValidation;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Domain.Interfaces.Services;
using Inventory.Domain.Services;
using Inventory.Infra.Data;
using Inventory.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
		services.AddDomainServices();
		services.AddFluentValidations();
		services.AddJwtAuthentication(configuration);

		return services;
	}

	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}

	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
		services.AddScoped<IUserService, UserService>();

		return services;
	}

	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, TokenService>();

		return services;
	}

	public static IServiceCollection AddFluentValidations(this IServiceCollection services)
	{
		return services.AddValidatorsFromAssembly(Assembly.Load("Inventory.Domain"), ServiceLifetime.Transient);
	}

	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("TokenJwt"));
		services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

		return services;
	}
}
