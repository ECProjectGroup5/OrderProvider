using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ShippingChoice
{
    [Required]
    public string Id { get; set; } = null!;

    [Required]
    public string ShippingCompanyName { get; set; } = null!;

    [Required]
    public string ShippingMethod { get; set; } = null!;
    public decimal ShippingPrice { get; set; }
}