namespace Infrastructure.Models;

public class OrderModel
{
	public string Id { get; set; } = null!;
	public string UserId { get; set; } = null!;
	public AddressModel Address { get; set; } = null!;
	public string ShippingChoice { get; set; } = null!;
    public string? DeliveryAddress { get; set; }
    public IEnumerable<ProductModel> ProductList { get; set; } = null!;
    public string? PromoCode { get; set; }
    public decimal PriceTotal { get; set; }
	public bool IsConfirmed { get; set; } = false;
    public DateTime? DeliveryDate { get; set; }
}
