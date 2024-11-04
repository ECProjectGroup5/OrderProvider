namespace Infrastructure.Models;

public class ProductModel
{
	public string ProductId { get; set; } = null!;
	public string ProductName { get; set; } = null!;
	public string ProductDescription { get; set; } = null!;
	public decimal ProductPrice { get; set; }
}
