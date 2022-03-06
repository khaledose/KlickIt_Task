using Business.Exceptions;
using Business.IBusinessLogic;
using Data.IUnitsOfWork;
using Domain;
using Domain.Entities;
using Domain.Constants;
using System.Net;
using Business.ApiRequests.RequestModels;

namespace Business.BusinessLogic;

public class RequestsBusiness : IRequestsBusiness
{
    private IUnitOfWork _unitOfWork { get; init; }

    public RequestsBusiness(IUnitOfWork unitOfWork) 
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Request> AddRequest(AddRequestModel request)
    {
        Request newRequest = new Request
        {
            UserId = request.UserId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Status = RequestStatus.Pending
        };
        _unitOfWork.Requests.Create(newRequest);
        await _unitOfWork.SaveChangesAsync();
        return newRequest;
    }

    public async Task DeleteRequest(Id<Request> id)
    {
        Request? existingRequest = await GetRequest(id);
        if (existingRequest is null)
        {
            throw new HttpStatusException($"Request with Id {id} was not found!", HttpStatusCode.NotFound);
        }

        if(existingRequest.Status != RequestStatus.Pending)
        {
            throw new HttpStatusException($"You can only delete pending requests", HttpStatusCode.BadRequest);
        }

        _unitOfWork.Requests.Delete(existingRequest);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<Request>> GetAllRequests(GetRequestsModel request)
    {
        return await _unitOfWork.Requests.GetAllUserRequests(
            request.UserId,
            request.Status,
            request.SearchTerm,
            request.PageNumber);
    }

    public async Task<Request> GetRequest(Id<Request> id)
    {
        Request? request = await _unitOfWork.Requests.GetById(id);
        if (request is null)
        {
            throw new HttpStatusException($"Request with Id {id} was not found!", HttpStatusCode.NotFound);
        }
        return request;
    }

    public async Task<Request> UpdateRequest(Id<Request> id, UpdateRequestModel request)
    {
        Request? existingRequest = await GetRequest(id);

        existingRequest.ProductId = request.ProductId ?? existingRequest.ProductId;
        existingRequest.Quantity = request.Quantity ?? existingRequest.Quantity;

        await _unitOfWork.SaveChangesAsync();
        return existingRequest;
    }

    public async Task<Request> UpdateRequestStatus(Id<Request> id, UpdateRequestModel request)
    {
        Request? existingRequest = await GetRequest(id);

        existingRequest.Status = request.Status ?? existingRequest.Status;

        await _unitOfWork.SaveChangesAsync();
        return existingRequest;
    }

    public async Task<int> GetRequestsCount(GetRequestsModel request)
    {
        return await _unitOfWork.Requests.GetRequestsCount(
            request.UserId,
            request.Status,
            request.SearchTerm);
    }
}
