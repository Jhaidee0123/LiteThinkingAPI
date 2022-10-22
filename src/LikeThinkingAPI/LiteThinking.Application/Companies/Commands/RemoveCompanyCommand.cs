using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Companies.Commands;

public class RemoveCompanyCommand : IRequest
{
    [Required]
    public Guid CompanyId { get; set; }
}
