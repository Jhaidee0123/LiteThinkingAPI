using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Companies.Commands;

public class CreateCompanyCommand : IRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public long Nit { get; set; }

    [Required]
    public string Phone { get; set; } = string.Empty;
}
