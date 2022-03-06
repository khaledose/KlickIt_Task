namespace Domain.Entities;

public partial class Product
{
    public Product()
    {
        Requests = new HashSet<Request>();
    }

    public Id<Product> Id { get; set; } = new Id<Product>(Guid.NewGuid());
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
}
