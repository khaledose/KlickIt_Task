namespace Business.ApiRequests.ProductModels;

public class GetProductsModel
{
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; }
}
