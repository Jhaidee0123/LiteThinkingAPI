using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Ports.Repositories;
using Scriban;
using System.Globalization;
using System.Reflection;

namespace LiteThinking.Infrastructure.Repositories;

public class PdfGenerator : IPdfGenerator
{
    public string GeneratePdfFile(List<Article> articles, string inventoryName) 
    {
        var template = GetHtmlTemplate();
        return template.Render(new
        {
            InventoryName = inventoryName,
            Articles = articles
        });
    }

    private Template GetHtmlTemplate()
    {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var fileRoute = String.Format(CultureInfo.InvariantCulture, "{0}/Templates/{1}", currentDirectory, "Template.html");
        var htmlTemplate = File.ReadAllText(fileRoute);
        return Template.Parse(htmlTemplate);
    }
}
