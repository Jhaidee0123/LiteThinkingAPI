using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiteThinking.Application.Articles.Commands;

public class AddArticleToInventoryCommandHandler : AsyncRequestHandler<AddArticleToInventoryCommand>
{
    private readonly IRepository<Inventory> _inventoryRepository;

    public AddArticleToInventoryCommandHandler(IRepository<Inventory> inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    protected override async Task Handle(AddArticleToInventoryCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var inventory = await _inventoryRepository.Query()
            .Where(inv => inv.Id == request.InventoryId)
            .Include(inv => inv.Articles)
            .FirstOrDefaultAsync(cancellationToken);

        inventory.AddArticle(request.Name, request.Quantity);

        await _inventoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
