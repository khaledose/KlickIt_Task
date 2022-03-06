using Domain.Constants;

namespace Business.ApiRequests.RequestModels;

public class GetRequestsModel
{
    public Guid? UserId { get; set; }
    public string? SearchTerm { get; set; }
    public RequestStatus? Status { get; set; }
    public int PageNumber { get; set; }
}
