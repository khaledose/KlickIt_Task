using Domain;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.ApiRequests.RequestModels;

public class AddRequestModel
{
    [Required(ErrorMessage = "UserId is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Product Id is required")]
    public Id<Product> ProductId { get; set; } = null!;

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
}
