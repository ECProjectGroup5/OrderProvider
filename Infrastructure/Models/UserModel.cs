
namespace Infrastructure.Models
{
    public class UserModel
    {
        public string Id { get; set; } = null!;

        public AddressModel? Address { get; set; }

        public bool IsGuest { get; set; } = false;
    }
}
