using FluentValidation.Results;
using Inventory.API.Models.User;
using Inventory.Application.Interfaces;
using Inventory.Application.Models.User;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Domain.Interfaces.Services;
using Inventory.Domain.Utils;

namespace Inventory.Application.Services;

public class UserService : ServiceBase<User>, IUserService
{
	private readonly IUserRepository _repository;
	private readonly ITokenService _tokenService;

	public UserService(IUserRepository repository, ITokenService tokenService) : base(repository)
	{
		_repository = repository;
		_tokenService = tokenService;
	}

	public async Task<bool> Validate(User user)
	{
		var existingEmail = await _repository.GetSingleAsync(x => x.Email == user.Email);
		if (existingEmail != null)
			user.ValidationResult.Errors.Add(new ValidationFailure(nameof(user.Email), "This Email is already being used"));

		return user.ValidationResult.IsValid;
	}

	public async Task<RegisterResponse> RegisterAsync(User user)
	{
		user.HashPassword();
		await _repository.AddAsync(user);

		return new RegisterResponse(user.Id, user.Name, user.Email, user.IsActive, user.CreatedAt, user.UpdatedAt);
	}

	public async Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest request)
	{
		var user = await _repository.GetSingleAsync(x => x.Email == request.Email);

		// validate
		if (user == null || !PasswordUtils.VerifyPassword(request.Password, user.Password))
			return null;

		var token = _tokenService.GenerateToken(user);
		var refreshToken = string.Empty;
		// // authentication successful so generate jwt and refresh tokens
		// var jwtToken = _jwtUtils.GenerateJwtToken(user);
		// var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
		// user.RefreshTokens.Add(refreshToken);
		//
		// // remove old refresh tokens from user
		// removeOldRefreshTokens(user);
		//
		// // save changes to db
		// _context.Update(user);
		// _context.SaveChanges();
		//
		// return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
		return new AuthenticateResponse(user.Id, user.Name, user.Email, token, refreshToken);
	}
}
