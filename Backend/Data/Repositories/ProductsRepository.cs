using Data.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProductsRepository : GenericRepository<Product>, IProductsRepository
{
    public ProductsRepository(KlickItContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetPaginatedProducts(string? searchTerm, int pageNumber)
    {
        int take = 5;
        int skip = (pageNumber - 1) * take;

        return await _context.Products.Where(
            P => searchTerm == null ||P.Name.ToLower().Contains(searchTerm.ToLower()))
            .OrderBy(P => P.Name)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<int> GetProductsCount(string? searchTerm)
    {
        return await _context.Products.Where(
            P => searchTerm == null || P.Name.ToLower().Contains(searchTerm.ToLower())).CountAsync();
    }
}
