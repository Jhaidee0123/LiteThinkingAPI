using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;

namespace LiteThinking.Application.Companies.Commands;

public class EditCompanyCommandHandler : AsyncRequestHandler<EditCompanyCommand>
{
    private readonly IRepository<Company> _companyRepository;

    public EditCompanyCommandHandler(IRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    protected async override Task Handle(EditCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(x => x.Id == request.CompanyId);

        company.Name = request.Name;
        company.Address = request.Address;
        company.Phone = request.Phone;
        company.Nit = request.Nit;

        await _companyRepository.UnitOfWork.SaveChangesAsync();
    }
}
