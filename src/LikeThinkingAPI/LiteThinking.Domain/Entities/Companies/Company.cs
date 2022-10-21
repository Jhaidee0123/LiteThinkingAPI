namespace LiteThinking.Domain.Entities.Companies;

public class Company : BaseEntity
{
    private Company() { }

    public Company(string name, string address, long nit, string phone)
    {
        Name = name;
        Address = address;
        Nit = nit;
        Phone = phone;
    }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public long Nit { get; set; }

    public string Phone { get; set; } = string.Empty;

    public Inventory Inventory { get; private set; }

    public void CreateInventory(string name, string description) 
    {
        Inventory ??= new Inventory(name, description);
    }
}
