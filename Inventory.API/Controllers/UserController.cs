using Inventory.Application.Interfaces;
using Inventory.Application.Models.User;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[Route("register")]
	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request)
	{
		var user = new User(request.Name, request.Email, request.Password);

		if (!(user.IsValid() && await _userService.Validate(user)))
			return BadRequest(user.GetErrors());

		var response = await _userService.RegisterAsync(user);
		return Ok(response);
	}

	[Route("authenticate")]
	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult> Authenticate([FromBody] AuthenticateRequest request)
	{
		var login = await _userService.AuthenticateAsync(request);
		if (login == null)
			return NotFound(new { error = "Invalid username or password." });

		return Ok(login);
	}
}
