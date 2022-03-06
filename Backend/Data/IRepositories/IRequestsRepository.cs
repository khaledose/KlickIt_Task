using Domain.Constants;
using Domain.Entities;

namespace Data.IRepositories;

public interface IRequestsRepository : IGenericRepository<Request>
{
    public Task<List<Request>> GetAllUserRequests(Guid? userId, RequestStatus? status, string? searchTerm, int pageNumber);

    public Task<int> GetRequestsCount(Guid? userId, RequestStatus? status, string? searchTerm);
}
