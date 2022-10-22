using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;

namespace LiteThinking.Application.Companies.Commands;

public class RemoveCompanyCommandHandler : AsyncRequestHandler<RemoveCompanyCommand>
{
    private readonly IRepository<Company> _companyRepository;

    public RemoveCompanyCommandHandler(IRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    protected async override Task Handle(RemoveCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(x => x.Id == request.CompanyId);

        _companyRepository.Remove(company);

        await _companyRepository.UnitOfWork.SaveChangesAsync();
    }
}
