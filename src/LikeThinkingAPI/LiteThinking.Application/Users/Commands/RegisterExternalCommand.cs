using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Users.Commands;

public class RegisterExternalCommand : IRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
