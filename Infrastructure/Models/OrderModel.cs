namespace Infrastructure.Models;

public class OrderModel
{
	public string OrderId { get; set; } = null!;
	public AddressModel Address { get; set; } = null!;
	public IEnumerable<ProductModel> Product { get; set; } = null!;
}
