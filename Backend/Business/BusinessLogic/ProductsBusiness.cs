using Business.ApiRequests.ProductModels;
using Business.Exceptions;
using Business.IBusinessLogic;
using Data.IUnitsOfWork;
using Domain;
using Domain.Entities;
using System.Net;

namespace Business.BusinessLogic;

public class ProductsBusiness : IProductsBusiness
{
    private IUnitOfWork _unitOfWork { get; init; }
    
    public ProductsBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Product> AddProduct(AddProductModel request)
    {
        Product product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };
        _unitOfWork.Products.Create(product);
        await _unitOfWork.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProduct(Id<Product> id)
    {
        Product existingProduct = await GetProduct(id);

        _unitOfWork.Products.Delete(existingProduct);
        
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProducts(GetProductsModel request)
    {
        return await _unitOfWork.Products.GetPaginatedProducts(request.SearchTerm, request.PageNumber);
    }

    public async Task<Product> GetProduct(Id<Product> id)
    {
        Product? product = await _unitOfWork.Products.GetById(id);
        if (product is null)
        {
            throw new HttpStatusException($"Product with Id {id} was not found!", HttpStatusCode.NotFound);
        }
        return product;
    }

    public async Task<Product> UpdateProduct(Id<Product> id, UpdateProductModel request)
    {
        Product? product = await GetProduct(id);

        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        product.Price = request.Price ?? product.Price;

        await _unitOfWork.SaveChangesAsync();
        return product;
    }

    public async Task<int> GetProductsCount(GetProductsModel request)
    {
        return await _unitOfWork.Products.GetProductsCount(request.SearchTerm);
    }
}
