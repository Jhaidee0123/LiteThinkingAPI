namespace LiteThinking.Domain.Entities.Companies;

public class Inventory : BaseEntity
{
    public Inventory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public Company Company { get; private set; }

    public Guid CompanyId { get; private set; }

    public ICollection<Article> Articles { get; private set; }

    public void AddArticle(string name, int quantity)
    {
        Articles ??= new List<Article>();

        Articles.Add(new Article(name, quantity));
    }
}
