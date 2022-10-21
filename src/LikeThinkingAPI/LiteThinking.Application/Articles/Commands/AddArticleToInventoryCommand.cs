using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LiteThinking.Application.Articles.Commands;

public class AddArticleToInventoryCommand : IRequest
{
    [Required]
    public Guid InventoryId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }
}
