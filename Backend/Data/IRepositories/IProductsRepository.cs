using Domain.Entities;

namespace Data.IRepositories;

public interface IProductsRepository : IGenericRepository<Product>
{
    public Task<List<Product>> GetPaginatedProducts(string? searchTerm, int pageNumber);

    public Task<int> GetProductsCount(string? searchTerm);
}
