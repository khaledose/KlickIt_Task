using Business.Exceptions;
using Business.IBusinessLogic;
using Domain;
using Domain.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.Responses;
using Business.ApiRequests.ProductModels;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private IProductsBusiness _productsBusiness { get; init; }
    private ILogger<ProductsController> _logger { get; init; }

    public ProductsController(IProductsBusiness productsBusiness, ILogger<ProductsController> logger)
    {
        _productsBusiness = productsBusiness;
        _logger = logger;   
    }

    [HttpPost, Authorize]
    public async Task<Response<Product>> GetAllProducts(GetProductsModel request)
    {
        Response<Product> response = new Response<Product>();
        try
        {
            response.Entities = await _productsBusiness.GetAllProducts(request);
            response.Count = await _productsBusiness.GetProductsCount(request);
            response.SetSuccess();
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }

    [HttpGet("{id}"), Authorize]
    public async Task<Response<Product>> GetProduct(Guid id)
    {
        Response<Product> response = new Response<Product>();
        try
        {
            response.Entity = await _productsBusiness.GetProduct(new Id<Product>(id));
            response.SetSuccess();
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }

    [HttpPost("New-Product"), Authorize(Roles = Roles.Admin)]
    public async Task<Response<Product>> CreateProduct([FromBody] AddProductModel request)
    {
        Response<Product> response = new Response<Product>();
        try
        {
            response.Entity = await _productsBusiness.AddProduct(request);
            response.SetSuccess();
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }

    [HttpPatch("{id}"), Authorize(Roles = Roles.Admin)]
    public async Task<Response<Product>> UpdateProduct(Guid id, [FromBody] UpdateProductModel request)
    {
        Response<Product> response = new Response<Product>();
        try
        {
            response.Entity = await _productsBusiness.UpdateProduct(new Id<Product>(id), request);
            response.SetSuccess();
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }

    [HttpDelete("{id}"), Authorize(Roles = Roles.Admin)]
    public async Task<Response<Product>> DeleteProduct(Guid id)
    {
        Response<Product> response = new Response<Product>();
        try
        {
            await _productsBusiness.DeleteProduct(new Id<Product>(id));
            response.SetSuccess();
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }
}
