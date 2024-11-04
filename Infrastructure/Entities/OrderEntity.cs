using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class OrderEntity
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string ProductList { get; set; } = null!;

        [Required]
        public decimal PriceTotal { get; set; }
    }
}
