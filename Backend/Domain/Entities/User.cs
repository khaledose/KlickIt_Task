using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public User() : base()
    {
        Requests = new HashSet<Request>();
    }

    public bool IsActive { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
}
