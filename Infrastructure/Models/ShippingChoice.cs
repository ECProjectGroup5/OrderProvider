namespace Infrastructure.Models;

public class ShippingChoice
{
    public string Id { get; set; } = null!;
    public string ShippingCompanyName { get; set; } = null!;
    public string ShippingMethod { get; set; } = null!;
    public decimal ShippingPrice { get; set; }
}