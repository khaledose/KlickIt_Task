using Data.IRepositories;
using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RequestsRepository : GenericRepository<Request>, IRequestsRepository
{
    public RequestsRepository(KlickItContext context) : base(context)
    {
    }

    public async Task<List<Request>> GetAllUserRequests(Guid? userId, RequestStatus? status, string? searchTerm, int pageNumber)
    {
        int take = 5;
        int skip = (pageNumber - 1) * take;

        return await _entities.Where(
            R => 
                (userId == null || R.UserId == userId) &&
                (status == null || R.Status == status) &&
                (searchTerm == null || R.Product.Name.ToLower().Contains(searchTerm.ToLower())))
            .OrderBy(R => R.UserId)
            .ThenBy(R => R.Status)
            .Skip(skip)
            .Take(take)
            .ToListAsync() ?? new();
    }

    public async Task<int> GetRequestsCount(Guid? userId, RequestStatus? status, string? searchTerm)
    {
        return await _entities.Where(
            R =>
                (userId == null || R.UserId == userId) &&
                (status == null || R.Status == status) &&
                (searchTerm == null || R.Product.Name.ToLower().Contains(searchTerm.ToLower()))).CountAsync();
    }
}
