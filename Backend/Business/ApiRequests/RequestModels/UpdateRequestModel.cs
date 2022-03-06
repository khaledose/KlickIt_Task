using Domain;
using Domain.Entities;
using Domain.Constants;

namespace Business.ApiRequests.RequestModels;

public class UpdateRequestModel
{
    public Id<Product>? ProductId { get; set; }
    public int? Quantity { get; set; }
    public RequestStatus? Status { get; set; }
}
