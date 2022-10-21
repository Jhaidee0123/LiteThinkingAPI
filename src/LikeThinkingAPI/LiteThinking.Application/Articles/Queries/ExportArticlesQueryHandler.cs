using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiteThinking.Application.Articles.Queries;

public class ExportArticlesQueryHandler : IRequestHandler<ExportArticlesQuery, string>
{
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IPdfGenerator _pdfGenerator;

    public ExportArticlesQueryHandler(IRepository<Inventory> inventoryRepository, IPdfGenerator pdfGenerator)
    {
        _inventoryRepository = inventoryRepository;
        _pdfGenerator = pdfGenerator;
    }

    public async Task<string> Handle(ExportArticlesQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.Query()
            .Where(x => x.Id == request.InventoryId)
            .Include(x => x.Articles)
            .FirstOrDefaultAsync();
        return _pdfGenerator.GeneratePdfFile(inventory.Articles.ToList(), inventory.Name);
    }
}
