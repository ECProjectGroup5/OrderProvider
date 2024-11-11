using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class CreateOrderModel
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public ShippingChoice ShippingChoice { get; set; } = null!;

    [Required]
    public string DeliveryAddress { get; set; } = null!;

    [Required]
    public IEnumerable<ProductModel> ProductList { get; set; } = null!;

    [DefaultValue(false)]
    public bool PaymentIsConfirmed { get; set; } = false;
}
