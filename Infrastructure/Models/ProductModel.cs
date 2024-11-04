namespace Infrastructure.Models;

public class ProductModel
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public int InStock { get; set; }
	public decimal Price { get; set; }
}
