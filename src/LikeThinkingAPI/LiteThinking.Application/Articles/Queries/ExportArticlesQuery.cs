using LiteThinking.Domain.Entities.Companies;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Articles.Queries;

public class ExportArticlesQuery : IRequest<string>
{
    public ExportArticlesQuery(Guid inventoryId)
    {
        InventoryId = inventoryId;
    }

    [Required]
    public Guid InventoryId { get; set; }
}
