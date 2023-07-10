using Inventory.API.Helpers;
using Inventory.Application.Interfaces;
using Inventory.Application.Models.Company;
using Inventory.Domain.Entities;
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

		var response = companies.Select(cp =>
			new CompanyResponse(cp.Id, cp.Name, cp.Document, cp.IsActive, cp.CreatedAt, cp.UpdatedAt));

		return Ok(response);
	}

	[HttpGet("{id:int}", Name = nameof(GetCompanyById))]
	public async Task<IActionResult> GetCompanyById(int id)
	{
		var company = await _companyService.GetSingleAsync(x => x.Id == id);

		if (company == null)
			return NotFound();

		var response = new CompanyResponse(company.Id, company.Name, company.Document, company.IsActive, company.CreatedAt,
			company.UpdatedAt);

		return Ok(response);
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] CompanyPostRequest request)
	{
		var company = new Company(request.Name, request.Document, request.IsActive);

		if (!company.IsValid())
		{
			var error = company.GetErrors();
			if (error == null)
				return BadRequest();

			return BadRequest(error);
		}

		await _companyService.AddAsync(company);

		var response = new CompanyResponse(company.Id, company.Name, company.Document, company.IsActive,
			company.CreatedAt,
			company.UpdatedAt);

		return new CreatedAtRouteResult(nameof(GetCompanyById), new { id = response.Id },
			response);
	}
}
