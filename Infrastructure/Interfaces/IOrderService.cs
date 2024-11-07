using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
	bool CreateOrderAsync(OrderModel model);
	OrderModel GetOneOrderAsync(string id);
    OrderModel GetOneUserOrderAsync(string id, UserModel user);
    IEnumerable<OrderModel> GetAllUserOrdersAsync();
	bool UpdateOrderAsync(OrderModel model);
	bool DeleteOrderAsync(OrderModel model);
	AddressModel GetAddressAsync(string id);
	IEnumerable<ProductModel> GetProductListAsync(string id);
	CartModel GetUserCartAsync(string userId);
}
