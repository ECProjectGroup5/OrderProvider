using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class OrderModel
{
    [Required]
	public string Id { get; set; } = null!;

    [Required]
    public UserModel User { get; set; } = null!;

    [Required]
    public ShippingChoice ShippingChoice { get; set; } = null!;

    public string? DeliveryAddress { get; set; }

    [Required]
    public IEnumerable<ProductModel> ProductList { get; set; } = null!;
    public PromoCodeModel? PromoCode { get; set; }
    public decimal PriceTotal { get; set; }
	public bool IsConfirmed { get; set; } = false;
	public string? OrderStatus { get; set; }
	public DateTime? DeliveryDate { get; set; }
    public DateTime? CreationDate { get; set; }
}
