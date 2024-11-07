using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class OrderEntity
    {
        [Key]
        public string Id { get; set; } = null!; //Order number

        [Required]
        public string UserId { get; set; } = null!; //Id of the user who the order belongs to

        [Required]
        public string Address { get; set; } = null!; //The home address entered in the order

        [Required]
        public string ShippingChoice { get; set; } = null!; //The chosen method of delivery

        public string? DeliveryAddress { get; set; } //The actual delivery address, which will be the same as Address in the case of home delivery

        [Required]
        public string ProductList { get; set; } = null!; //JSON string of the list of product objects

        public string? PromoCode { get; set; } //If a promo code is used, the promo code object (which consists of an Id and a percentage value) will be stored as a JSON string here

        [Required]
        public decimal PriceTotal { get; set; } //The total price of the order, including shipping choice and promo code

        [Required]
        public string Status { get; set; } = "Accepted"; //The delivery status of the order (Accepted, In Transit, Delivered). Is Accepted by default.

        [Column(TypeName = "datetime2")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now.AddDays(7); //The date of delivery. Will first be an estimate, but will then be updated to the actual date that the order was delivered. Is set to 7 days ahead as default

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}


