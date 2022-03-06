using Business.ApiRequests.RequestModels;
using Domain;
using Domain.Entities;

namespace Business.IBusinessLogic;

public interface IRequestsBusiness
{
    public Task<Request> GetRequest(Id<Request> id);

    public Task<List<Request>> GetAllRequests(GetRequestsModel request);

    public Task<Request> AddRequest(AddRequestModel request);

    public Task<Request> UpdateRequest(Id<Request> id, UpdateRequestModel request);

    public Task<Request> UpdateRequestStatus(Id<Request> id, UpdateRequestModel request);

    public Task DeleteRequest(Id<Request> id);

    public Task<int> GetRequestsCount(GetRequestsModel request);
}
