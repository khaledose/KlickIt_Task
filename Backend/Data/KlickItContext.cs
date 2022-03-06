using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Data;
    
public class KlickItContext : IdentityDbContext<User, Role, Guid>
{
    public KlickItContext(DbContextOptions<KlickItContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Request> Requests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity => 
        { 
            entity.ToTable(name: "Users");
            entity.HasQueryFilter(m => EF.Property<bool>(m, "IsActive") == true);
        });
        modelBuilder.Entity<Role>(entity => 
        { 
            entity.ToTable(name: "Roles");
            entity.HasQueryFilter(m => EF.Property<bool>(m, "IsActive") == true);
        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).HasConversion(from => from.Value, to => new Id<Product>(to));
            entity.HasQueryFilter(m => EF.Property<bool>(m, "IsActive") == true);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(e => e.Id).HasConversion(from => from.Value, to => new Id<Request>(to));

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Products");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Users");

            entity.HasQueryFilter(m => EF.Property<bool>(m, "IsActive") == true);
        });
    }

    public override int SaveChanges()
    {
        OnBeforeSaveChanges();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Metadata.FindProperty("IsActive") is null)
            {
                continue;
            }
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.CurrentValues["IsActive"] = true;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsActive"] = false;
                    break;
            }
        }
    }
}

