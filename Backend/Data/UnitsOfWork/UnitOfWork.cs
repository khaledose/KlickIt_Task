using Data.IRepositories;
using Data.IUnitsOfWork;
using Data.Repositories;

namespace Data.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    private KlickItContext _context { get; init; }
    public IUsersRepository Users { get; init; }
    public IProductsRepository Products { get; init; }
    public IRequestsRepository Requests { get; init; }

    public UnitOfWork(KlickItContext context)
    {
        _context = context;
        Users = new UsersRepository(_context);
        Products = new ProductsRepository(_context);
        Requests = new RequestsRepository(_context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
