namespace Infrastructure.Models;

public class PromoCodeModel
{
    public string Id { get; set; } = null!;
    public string PromoCode { get; set;} = null!;
    public decimal DiscountPercentage { get; set; }
}
