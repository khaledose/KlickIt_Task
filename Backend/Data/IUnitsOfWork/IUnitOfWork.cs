using Data.IRepositories;

namespace Data.IUnitsOfWork;

public interface IUnitOfWork
{
    IUsersRepository Users { get; init; }
    IProductsRepository Products { get; init; }
    IRequestsRepository Requests { get; init; }
    public Task SaveChangesAsync();
    public void SaveChanges();
    public Task DisposeAsync();
    public void Dispose();
}
