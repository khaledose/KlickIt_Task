using System.ComponentModel.DataAnnotations;

namespace Business.ApiRequests.ProductModels;

public class AddProductModel
{
    [Required(ErrorMessage = "Product Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Product Description is required")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Product Price is required")]
    public double Price { get; set; }
}
