namespace LiteThinking.Domain.Entities.Companies;

public class Article : BaseEntity
{
    public Article(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }

    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public Guid InventoryId { get; private set; }
    
    public Inventory Inventory { get; private set; }
}
