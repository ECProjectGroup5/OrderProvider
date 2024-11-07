namespace Infrastructure.Models;

public class CartModel
{
    public string UserId { get; set; } = null!;
    public IEnumerable<ProductModel> ProductList { get; set; } = null!;
    public string? PromoCode { get; set; }
    public decimal PriceTotal { get; set; }
}
