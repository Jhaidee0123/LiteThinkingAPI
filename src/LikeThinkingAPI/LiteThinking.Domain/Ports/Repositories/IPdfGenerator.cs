using LiteThinking.Domain.Entities.Companies;

namespace LiteThinking.Domain.Ports.Repositories;

public interface IPdfGenerator
{
    string GeneratePdfFile(List<Article> articles, string inventoryName);
}
