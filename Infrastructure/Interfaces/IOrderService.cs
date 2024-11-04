using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
	bool CreateOrder(OrderModel model);
	OrderModel GetOneOrder(string id);
	IEnumerable<OrderModel> GetAllOrders();
	bool UpdateOrder(OrderModel model);
	bool DeleteOrder(OrderModel model);
	AddressModel GetAddress(string id);
	IEnumerable<ProductModel> GetProductList(string id);
}
