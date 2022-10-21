using LiteThinking.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LiteThinking.Api.Controllers;

[Route("api/[controller]")]
[EnableCors("Cors")]
[ApiController]
public class ExternalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExternalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// POST /api/externals/register
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterExternalCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }
}
