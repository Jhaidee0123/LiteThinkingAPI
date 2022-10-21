using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiteThinking.Application.Companies.Queries;

public class ListCompaniesQueryHandler : IRequestHandler<ListCompaniesQuery, IEnumerable<Company>>
{
    private readonly IRepository<Company> _companyRepository;

    public ListCompaniesQueryHandler(IRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<Company>> Handle(ListCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _companyRepository.Query()
            .Where(x => true)
            .Include(x => x.Inventory)
                .ThenInclude(x => x.Articles)
            .ToListAsync();
    }
}
