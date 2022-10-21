using LiteThinking.Application.Authentication.Queries;
using LiteThinking.Application.Users;
using LiteThinking.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiteThinking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("Cors")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public UsersController(UserManager<User> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    /// <summary>
    /// Returns the info for the currently logged in user
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfileAsync()
    {
        var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByNameAsync(userId);
        var roles = await _userManager.GetRolesAsync(user);
        var userProfile = new UserProfile(user.Email, roles.First());
        return Ok(userProfile);
    }

    /// <summary>
    /// Authenticates the user and returns a JWT
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}
