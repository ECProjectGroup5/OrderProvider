namespace Infrastructure.Models;

public class OrderModel
{
	public string Id { get; set; } = null!;
	public string UserId { get; set; } = null!;
	public AddressModel Address { get; set; } = null!;
	public IEnumerable<ProductModel> Products { get; set; } = null!;
	public decimal PriceTotal { get; set; }
	public bool IsConfirmed { get; set; } = false;
}
