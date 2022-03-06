using Domain.Constants;

namespace Domain.Entities;

public partial class Request
{
    public Id<Request> Id { get; set; } = new Id<Request>(Guid.NewGuid());
    public Id<Product> ProductId { get; set; } = new Id<Product>(Guid.NewGuid());
    public Guid UserId { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }
    public RequestStatus Status { get; set; }
    public bool IsActive { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
