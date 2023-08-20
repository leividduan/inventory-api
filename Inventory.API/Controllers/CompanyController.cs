using Inventory.API.Helpers;
using Inventory.Application.Interfaces;
using Inventory.Application.Models.Company;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

[Route("api/v1/companies")]
[Authorize]
[ApiController]
public class CompanyController : ControllerBase
{
	private readonly ICompanyService _companyService;

	public CompanyController(ICompanyService companyService)
	{
		_companyService = companyService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var user = HttpContext.User.GetUserData();

		var companies =
			await _companyService.GetAsync(x => x.CompanyUser.Any(i => i.IdUser == user.IdUser),
				"CompanyUser");

		var response = companies.Select(cp =>
			new CompanyResponse(cp.Id, cp.Name, cp.Document, cp.IsActive, cp.CreatedAt, cp.UpdatedAt));

		return Ok(response);
	}

	[HttpGet("{id:int}", Name = nameof(GetCompanyById))]
	public async Task<IActionResult> GetCompanyById(int id)
	{
		var user = HttpContext.User.GetUserData();
		var company =
			await _companyService.GetSingleAsync(x => x.Id == id && x.CompanyUser.Any(i => i.IdUser == user.IdUser));

		if (company == null)
			return NotFound();

		var response = new CompanyResponse(company.Id, company.Name, company.Document, company.IsActive, company.CreatedAt,
			company.UpdatedAt);

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] CompanyPostRequest request)
	{
		var user = HttpContext.User.GetUserData();
		var company = new Company(request.Name, request.Document, request.IsActive);

		if (!company.IsValid())
		{
			var error = company.GetErrors();
			if (error == null)
				return BadRequest();

			return BadRequest(error);
		}

		company.AssociateUser(user.IdUser, CompanyUser.Roles.Admin);
		await _companyService.AddAsync(company);

		var response = new CompanyResponse(company.Id, company.Name, company.Document, company.IsActive,
			company.CreatedAt,
			company.UpdatedAt);

		return new CreatedAtRouteResult(nameof(GetCompanyById), new { id = response.Id },
			response);
	}

	[HttpPost("{id:int}/associate-user/{idUser:int}")]
	public async Task<IActionResult> AssociateUser(int id, int idUser, [FromBody] AssociateUserRequest request)
	{
		var user = HttpContext.User.GetUserData();

		if (!Enum.TryParse(request.role, out CompanyUser.Roles enumRole))
			return BadRequest();

		return await _companyService.AssociateUserAsync(user.IdUser, id, idUser, enumRole)
			? NoContent()
			: BadRequest("You don't have access to this operation.");
	}

	[HttpPost("{id:int}/disassociate-user/{idUser:int}")]
	public async Task<IActionResult> DisassociateUser(int id, int idUser)
	{
		var user = HttpContext.User.GetUserData();

		return await _companyService.DisassociateUserAsync(user.IdUser, id, idUser)
			? NoContent()
			: BadRequest("You don't have access to this operation.");
	}
}
