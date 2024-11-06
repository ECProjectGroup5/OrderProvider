using Infrastructure.Interfaces;
using Infrastructure.Models;
using Moq;


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

	[Fact]

	public void DeleteOrderAsync_AdminShouldDeleteAnOrder_And_ReturnTrue()
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
		_orderService.Setup(x => x.GetOneOrderAsync(orderModel.Id)).Returns(orderModel);
		_orderService.Setup(x => x.DeleteOrderAsync(orderModel)).Returns(true);

		// Act

		var createResult = _orderService.Object.CreateOrderAsync(orderModel);
		var existingOrderResult = _orderService.Object.GetOneOrderAsync(orderModel.Id);
		var deleteResult = _orderService.Object.DeleteOrderAsync(existingOrderResult);

		// Assert
		Assert.Equal("Admin", userModel.Role);
		Assert.True(deleteResult);
	}

	[Fact]

	public void DeleteOrderAsync_AdminShouldNotDeleteAnOrder_And_ReturnFalse()
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
		_orderService.Setup(x => x.GetOneOrderAsync(orderModel.Id)).Returns(orderModel);


		// Act

		var createResult = _orderService.Object.CreateOrderAsync(orderModel);
		var existingOrderResult = _orderService.Object.GetOneOrderAsync("2");

		// Assert
		Assert.Equal("Admin", userModel.Role);
		Assert.Null(existingOrderResult);
	}
}
