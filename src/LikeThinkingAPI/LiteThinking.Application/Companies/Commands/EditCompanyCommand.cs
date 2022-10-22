using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Companies.Commands;

public class EditCompanyCommand : IRequest
{
    [Required]
    public Guid CompanyId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public long Nit { get; set; }

    [Required]
    public string Phone { get; set; } = string.Empty;
}
