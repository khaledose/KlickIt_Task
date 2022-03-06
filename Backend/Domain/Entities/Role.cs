using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public Role() : base()
    {
    }

    public Role(string roleName) : base(roleName)
    {
    }

    public bool IsActive { get; set; }

}
