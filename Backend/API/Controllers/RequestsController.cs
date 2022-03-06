using API.Responses;
using Business.ApiRequests.RequestModels;
using Business.Exceptions;
using Business.IBusinessLogic;
using Domain;
using Domain.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : Controller
{
    private IRequestsBusiness _requestsBusiness { get; init; }
    private ILogger<RequestsController> _logger { get; init; }  

    public RequestsController(IRequestsBusiness RequestsBusiness, ILogger<RequestsController> logger)
    {
        _requestsBusiness = RequestsBusiness;
        _logger = logger;
    }

    [HttpPost, Authorize]
    public async Task<Response<Request>> GetAllRequests(GetRequestsModel request)
    {
        Response<Request> response = new Response<Request>();
        try
        {
            response.Entities = await _requestsBusiness.GetAllRequests(request);
            response.Count = await _requestsBusiness.GetRequestsCount(request);
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
    public async Task<Response<Request>> GetRequest(Guid id)
    {
        Response<Request> response = new Response<Request>();
        try
        {
            response.Entity = await _requestsBusiness.GetRequest(new Id<Request>(id));
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

    [HttpPost("New-Request"), Authorize(Roles = Roles.User)]
    public async Task<Response<Request>> CreateRequest([FromBody] AddRequestModel request)
    {
        Response<Request> response = new Response<Request>();
        try
        {
            response.Entity = await _requestsBusiness.AddRequest(request);
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

    [HttpDelete("{id}"), Authorize(Roles = Roles.User)]
    public async Task<Response<Request>> DeleteRequest(Guid id)
    {
        Response<Request> response = new Response<Request>();
        try
        {
            await _requestsBusiness.DeleteRequest(new Id<Request>(id));
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

    [HttpPatch("{id}"), Authorize(Roles = Roles.User)]
    public async Task<Response<Request>> UpdateRequest(Guid id, [FromBody] UpdateRequestModel request)
    {
        Response<Request> response = new Response<Request>();
        try
        {
            response.Entity = await _requestsBusiness.UpdateRequest(new Id<Request>(id), request);
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

    [HttpPatch("Status/{id}"), Authorize(Roles = Roles.Admin)]
    public async Task<Response<Request>> UpdateRequestStatus(Guid id, [FromBody] UpdateRequestModel request)
    {
        Response<Request> response = new Response<Request>();
        try
        {
            response.Entity = await _requestsBusiness.UpdateRequestStatus(new Id<Request>(id), request);
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
