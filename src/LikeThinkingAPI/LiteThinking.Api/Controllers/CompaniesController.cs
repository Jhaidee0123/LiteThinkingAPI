using LiteThinking.Application.Companies.Commands;
using LiteThinking.Application.Companies.Queries;
using LiteThinking.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LiteThinking.Api.Controllers;

[Route("api/[controller]")]
[EnableCors("Cors")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// POST /api/companies/create-company
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("create-company")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }

    /// <summary>
    /// GET /api/companies/list-companies
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("list-companies")]
    [Authorize]
    public async Task<IActionResult> ListCompanies()
    {
        return Ok(await _mediator.Send(new ListCompaniesQuery()));
    }
}
