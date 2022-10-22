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
    public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }

    /// <summary>
    /// PUT /api/companies/edit-company
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("edit-company")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> EditCompanyAsync([FromBody] EditCompanyCommand command)
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
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> ListCompaniesAsync()
    {
        return Ok(await _mediator.Send(new ListCompaniesQuery()));
    }

    /// <summary>
    /// DELETE /api/companies/remove-company
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("remove-company/{companyId}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> RemoveCompanyAsync([FromRoute] Guid companyId)
    {
        return Ok(await _mediator.Send(new RemoveCompanyCommand() { CompanyId = companyId }));
    }
}
