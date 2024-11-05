using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
	bool CreateOrderAsync(OrderModel model);
	OrderModel GetOneOrderAsync(string id);
	IEnumerable<OrderModel> GetAllOrdersAsync();
	bool UpdateOrderAsync(OrderModel model);
	bool DeleteOrderAsync(OrderModel model);
	AddressModel GetAddressAsync(string id);
	IEnumerable<ProductModel> GetProductListAsync(string id);
}
