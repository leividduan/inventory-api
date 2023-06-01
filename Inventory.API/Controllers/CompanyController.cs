using Inventory.API.Helpers;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

[Route("api/v1/companies")]
[ApiController]
public class CompanyController : ControllerBase
{
	private readonly ICompanyService _companyService;

	public CompanyController(ICompanyService companyService)
	{
		_companyService = companyService;
	}

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var user = HttpContext.User.GetUserData();

		var companies = await _companyService.GetAsync();
		return Ok(true);
	}
}
