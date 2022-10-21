using LiteThinking.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LiteThinking.Api.Controllers;

[Route("api/[controller]")]
[EnableCors("Cors")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// POST /api/admins/register
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterAdminCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }
}