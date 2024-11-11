using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ProductModel
{
	[Required]
	public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
	public int Stock { get; set; }
	public decimal Price { get; set; }
}
