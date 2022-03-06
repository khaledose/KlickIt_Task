using Business.ApiRequests.ProductModels;
using Domain;
using Domain.Entities;

namespace Business.IBusinessLogic;

public interface IProductsBusiness
{
    public Task<Product> GetProduct(Id<Product> id);
    public Task<List<Product>> GetAllProducts(GetProductsModel request);
    public Task<Product> AddProduct(AddProductModel request);
    public Task<Product> UpdateProduct(Id<Product> id, UpdateProductModel request);
    public Task DeleteProduct(Id<Product> id);
    public Task<int> GetProductsCount(GetProductsModel request);
}
