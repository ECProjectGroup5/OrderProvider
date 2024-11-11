﻿using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
	bool CreateOrderAsync(OrderModel model);
	OrderModel GetOneOrderAsync(string id);
    OrderModel GetOneUserOrderAsync(string id, UserModel user);
    IEnumerable<OrderModel> GetAllUserOrdersAsync();
	bool UpdateOrderAsync(OrderModel model);
	bool DeleteOrderAsync(OrderModel model);
	AddressModel GetUserAddressAsync(string userId);
    bool UpdateUserAddressAsync(string userId, AddressModel addressModel);
    IEnumerable<ProductModel> GetProductListAsync(string id);
    IEnumerable<ProductModel> DeleteProductFromList(List<ProductModel> productList, string productId, int amount); //amount = The amount of instances of the product you want to delete (in case of multiple instances of the same product in the list)
    CartModel GetUserCartAsync(string userId);
	decimal CalculateTotalPrice(decimal orderPrice, decimal valuePrice);
	bool ValidatePromoCode(string userInput, string promoCode);
	bool SendMessageAsync(string OrderStatus, string NewOrderStatus);
}
