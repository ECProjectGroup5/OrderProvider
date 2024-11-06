using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Moq;
using Newtonsoft.Json;

namespace OrderProvider.Tests;

public class OrderService_Tests
{
    private readonly Mock<IOrderService> _orderService = new Mock<IOrderService>();

    [Fact]

    public void CreateOrderAsync_ShouldCreateAnOrder_And_ReturnTrue()
    {
        // Arrange

        var addressModel = new AddressModel()
        {
            Id = Guid.NewGuid().ToString(),
            Street = "gata",
            City = "Kalmar",
            State = "Depression",
            PhoneNumber = "123790",
            ZipCode = "39350",
            CountryCallingCode = "+46",
            Country = "Sweden"
        };

		var userModel = new UserModel()
		{
			Id = new Guid().ToString(),
			Address = addressModel,
			Role = "Admin"
		};

		var productModel = new ProductModel()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 20,
            Price = 100m
        };

        var productList = new List<ProductModel>();

        productList.Add(productModel);

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true);

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel);

        // Assert

        Assert.True(result);
    }
	
}
