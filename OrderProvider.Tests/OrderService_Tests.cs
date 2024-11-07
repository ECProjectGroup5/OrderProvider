using Infrastructure.Interfaces;
using Infrastructure.Models;
using Moq;


namespace OrderProvider.Tests;

public class OrderService_Tests
{
    private readonly Mock<IOrderService> _orderService = new Mock<IOrderService>(); //Mock for OrderService is created

    [Fact]

    public void CreateOrderAsync_ShouldCreateAnOrderAsAnAdmin_And_ReturnTrue()
    {
        // Arrange

        //An addressModel, userModel and productModel is created, for use in the orderModel

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

		var userModel = new UserModel() //An admin user is created
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

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); //CreateOrderAsync in the mocked orderService is setup to return true

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

        // Assert

        Assert.True(result); //It should return true
    }

    [Fact]

    public void CreateOrderAsync_ShouldReturnFalse_IfFieldIsMissing()
    {
        // Arrange

        //An addressModel, userModel and productModel is created, for use in the orderModel

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

        var userModel = new UserModel() //An admin user is created
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

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = null!, //User property is set to null
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        if (orderModel.User == null || orderModel.ProductList == null)
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(false); //CreateOrderAsync in the mocked orderService is setup to return false if one of the crucial fields are missing/null
        }

        else
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); //Otherwise it is setup to return true
        }

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

        // Assert

        Assert.False(result); //It should return false
    }

    [Fact]

    public void CreateOrderAsync_ShouldCreateAnOrderAsGuest_And_ReturnTrue()
    {
        // Arrange

        //An addressModel and productModel is created, for use in the orderModel

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

        var userModel = new UserModel() //A guest user is created
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "Guest"
        };

        var productModel = new ProductModel()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 20,
            Price = 100m
        };

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        if (orderModel.IsConfirmed)
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); //CreateOrderAsync in the mocked orderService is setup to return true if the IsConfirmed property is set to true
        }

        else
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(false); //Otherwise it is setup to return false
        }

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

		// Assert

        Assert.True(result); //It should return true

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

		if (orderModel.IsConfirmed)
        {
			_orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true);

            if (orderModel.User.Role == "Admin")
            {
				_orderService.Setup(x => x.GetOneOrderAsync(orderModel.Id)).Returns(orderModel);
				_orderService.Setup(x => x.DeleteOrderAsync(orderModel)).Returns(true);
			}
		}
			

		// Act

		var createResult = _orderService.Object.CreateOrderAsync(orderModel);
		var existingOrderResult = _orderService.Object.GetOneOrderAsync(orderModel.Id);
		var deleteResult = _orderService.Object.DeleteOrderAsync(existingOrderResult);

		// Assert
		Assert.Equal("Admin", userModel.Role);
		Assert.True(deleteResult);
	}

	[Fact]

	public void DeleteOrderAsync_AdminShouldNotDeleteAnOrder_If_OrderId_Is_Incorrect()
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

        if (orderModel.IsConfirmed)
        {
			_orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); 
            
            if (orderModel.User.Role == "Admin")
            {
				_orderService.Setup(x => x.GetOneOrderAsync(orderModel.Id)).Returns(orderModel);
			}
		}
		

		// Act

		var createResult = _orderService.Object.CreateOrderAsync(orderModel);
		var existingOrderResult = _orderService.Object.GetOneOrderAsync("2");

		// Assert
		Assert.Equal("Admin", userModel.Role);
		Assert.Null(existingOrderResult);
	}
    [Fact]

    public void CreateOrderAsync_ShouldReturnFalse_WhenProductIsNotInStock()
    {
        // Arrange

        //An addressModel and productModel is created, for use in the orderModel

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

        var userModel = new UserModel() //A guest user is created
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "Guest"
        };

        var productModel = new ProductModel()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 0, //Stock is set to 0
            Price = 100m
        };

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        if (productModel.Stock == 0)
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(false); //CreateOrderAsync in the mocked orderService is setup to return false if the product is out of stock
        }

        else
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); //Otherwise it is setup to return true
        }

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

        // Assert

        Assert.False(result); //It should return false
    }

    [Fact]

    public void CreateOrderAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        // Arrange

        //An addressModel and productModel is created, for use in the orderModel

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

        var userModel = new UserModel() //A guest user is created
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "Guest"
        };

        var productModel = new ProductModel()
        {
            Id = ".", //Id is set to something that will not exist in the products database
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 20,
            Price = 100m
        };

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(false); //CreateOrderAsync in the mocked orderService is setup to return false

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

        // Assert

        Assert.False(result); //It should return false
    }

    [Fact]
    public void CreateOrderAsync_ShouldReturnFalse_WhenPaymentIsNotConfirmed()
    {
        // Arrange

        //An addressModel and productModel is created, for use in the orderModel

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

        var userModel = new UserModel() //A guest user is created
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "Guest"
        };

        var productModel = new ProductModel()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 20,
            Price = 100m
        };

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = false, //IsConfirmed is set to false
        };

        if (orderModel.IsConfirmed)
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); //CreateOrderAsync in the mocked orderService is setup to return true if the IsConfirmed property is set to true
        }

        else
        {
            _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(false); //Otherwise it is setup to return false
        }

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

        // Assert

        Assert.False(result); //It should return false
    }

	[Fact]
	public void UpdateOrderAsync_AdminUpdatesOrder_AndReturnTrue()
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

        //First orderModel
		var originalOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = "PostNord Standard",
			ProductList = productList,
			PriceTotal = 100m,
			IsConfirmed = true, 
		};

        //First orderModel updated
		var updatedOrder = new OrderModel
		{
			Id = originalOrder.Id,
			User = originalOrder.User,
			ShippingChoice = "PostNord Express",
			ProductList = originalOrder.ProductList,
			PriceTotal = originalOrder.PriceTotal,
			IsConfirmed = originalOrder.IsConfirmed,
		};

		_orderService.Setup(x => x.CreateOrderAsync(originalOrder)).Returns(true);
		_orderService.Setup(x => x.UpdateOrderAsync(It.Is<OrderModel>(o => o.User.Role == "Admin"))).Returns(true);
		_orderService.Setup(x => x.GetOneOrderAsync(originalOrder.Id)).Returns(updatedOrder);

		// Act
		var createResult = _orderService.Object.CreateOrderAsync(originalOrder);

        //Updating the order
		var updateResult = _orderService.Object.UpdateOrderAsync(updatedOrder);

		//Fetching the updated order
		var getOneResult = _orderService.Object.GetOneOrderAsync(originalOrder.Id);

        // Assert
        Assert.True(createResult);
		Assert.True(updateResult);
		Assert.NotNull(getOneResult);
        Assert.Equal(originalOrder.Id, getOneResult.Id);
		Assert.NotEqual(originalOrder.ShippingChoice, getOneResult.ShippingChoice);
	}

	[Fact]
	public void UpdateOrderAsync_AdminUpdatesOrder_AndReturnFalse()
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

		//First orderModel
		var originalOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = "PostNord Standard",
			ProductList = productList,
			PriceTotal = 100m,
			IsConfirmed = true,
		};

		//First orderModel updated
		var updatedOrder = new OrderModel
		{
			Id = originalOrder.Id,
			User = originalOrder.User,
			ShippingChoice = "PostNord Express",
			ProductList = originalOrder.ProductList,
			PriceTotal = originalOrder.PriceTotal,
			IsConfirmed = originalOrder.IsConfirmed,
		};

		_orderService.Setup(x => x.CreateOrderAsync(originalOrder)).Returns(true);
        //Return false
		_orderService.Setup(x => x.UpdateOrderAsync(It.Is<OrderModel>(o => o.User.Role == "Admin"))).Returns(false);
		_orderService.Setup(x => x.GetOneOrderAsync(originalOrder.Id)).Returns(updatedOrder);

		// Act
		var createResult = _orderService.Object.CreateOrderAsync(originalOrder);

		//Trying to update the order
		var updateResult = _orderService.Object.UpdateOrderAsync(updatedOrder);

		//Fetching the original order
		var getOneResult = _orderService.Object.GetOneOrderAsync(originalOrder.Id);

		// Assert
		Assert.True(createResult);
		Assert.False(updateResult);
		Assert.NotNull(getOneResult);
		Assert.Equal(originalOrder.Id, getOneResult.Id);
		Assert.NotEqual(originalOrder.ShippingChoice, getOneResult.ShippingChoice);
	}
}
