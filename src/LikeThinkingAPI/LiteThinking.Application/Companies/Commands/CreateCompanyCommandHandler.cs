using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;

namespace LiteThinking.Application.Companies.Commands;

public class CreateCompanyCommandHandler : AsyncRequestHandler<CreateCompanyCommand>
{
    private readonly IRepository<Company> _companyRepository;

    public CreateCompanyCommandHandler(IRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    protected override async Task Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var company = new Company(request.Name, request.Address, request.Nit, request.Phone);

        company.CreateInventory(request.Name + " Inventory", "Current Inventory of " + request.Name);

        await _companyRepository.CreateAsync(company);

        await _companyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
