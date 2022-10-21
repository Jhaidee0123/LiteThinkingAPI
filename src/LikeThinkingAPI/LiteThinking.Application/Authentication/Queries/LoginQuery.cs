using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Authentication.Queries;

public class LoginQuery : IRequest<LoginResponse>
{
    [Required(ErrorMessage = "User Name is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
