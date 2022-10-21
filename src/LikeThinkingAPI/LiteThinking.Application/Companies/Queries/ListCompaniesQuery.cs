using LiteThinking.Domain.Entities.Companies;
using MediatR;

namespace LiteThinking.Application.Companies.Queries;

public class ListCompaniesQuery : IRequest<IEnumerable<Company>>
{
}
