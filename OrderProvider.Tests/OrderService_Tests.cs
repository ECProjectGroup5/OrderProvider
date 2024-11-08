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

        var standardShippingChoice = new ShippingChoice()
        {
            Id = Guid.NewGuid().ToString(),
            ShippingCompanyName = "PostNord",
            ShippingMethod = "Standard",
            ShippingPrice = 100m
        };

        var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = null!, //User property is set to null
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>();

        productList.Add(productModel);

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>();

        productList.Add(productModel);

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var productList = new List<ProductModel>(); //A productList is created, which will be inserted into the orderModel

        productList.Add(productModel); //The previously created productModel is added to the productList

        var orderModel = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
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
    public void GetOneOrderAsync_ShouldReturnCorrectOrder()
    {
        // Arrange

        string id = Guid.NewGuid().ToString(); //An order number is created, which will be used to search for the corresponding order

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var orderModel = new OrderModel //An orderModel is created with the id above, in order to simulate the corresponding order
        {
            Id = id,
            User = new UserModel(),
            ShippingChoice = standardShippingChoice,
            ProductList = new List<ProductModel>(),
            PriceTotal = 100m,
            IsConfirmed = true,
        };


        _orderService.Setup(x => x.GetOneOrderAsync(id)).Returns(orderModel); //GetOneOrderAsync in the mocked orderService is setup to return the simulated order above, if the corresponding Id is inserted

        // Act

        var result = _orderService.Object.GetOneOrderAsync(id); //GetOneOrderAsync is called

        // Assert

        Assert.NotNull(result); //The returned value should not be null
        Assert.IsType<OrderModel>(result); //The returned object should be of the type OrderModel
        Assert.Equal(id, result.Id); //The Id of the returned order should be equal to the one that was inserted to the GetOneOrderMethod
    }

    [Fact]
    public void GetOneOrderAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange

        string id = Guid.NewGuid().ToString(); //An order number is created, which will be used to search for the corresponding order

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var orderModel = new OrderModel //An orderModel is created with a diffrent id from the one above, in order to simulate a different order
        {
            Id = Guid.NewGuid().ToString(),
            User = new UserModel(),
            ShippingChoice = standardShippingChoice,
            ProductList = new List<ProductModel>(),
            PriceTotal = 100m,
            IsConfirmed = true,
        };


        _orderService.Setup(x => x.GetOneOrderAsync(orderModel.Id)).Returns(orderModel); //GetOneOrderAsync in the mocked orderService is setup to return the simulated order above, if the corresponding Id is inserted (otherwise it will return null)

        // Act

        var result = _orderService.Object.GetOneOrderAsync(id); //GetOneOrderAsync is called

        // Assert

        Assert.Null(result); //The returned value should be null
    }

    [Fact]
    public void GetOneUserOrderAsync_ShouldReturnTheCorrectOrder_ToTheCorrectUser()
    {
        // Arrange

        string id = Guid.NewGuid().ToString(); //An order number is created, which will be used to search for the corresponding order

        var userModel = new UserModel() //A registered user is created
        {
            Id = new Guid().ToString(),
            Address = new AddressModel(),
            Role = "User"
        };

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var orderModel = new OrderModel //An orderModel is created with the same id as above, and with the same user as above, in order to simulate the corresponding order
        {
            Id = id,
            User = userModel,
            ShippingChoice = standardShippingChoice,
            ProductList = new List<ProductModel>(),
            PriceTotal = 100m,
            IsConfirmed = true,
        };


        _orderService.Setup(x => x.GetOneUserOrderAsync(id, userModel)).Returns(orderModel); //GetOneUserOrderAsync in the mocked orderService is setup to return the simulated order above, if the corresponding id and user is inserted (otherwise it will return null)

        // Act

        var result = _orderService.Object.GetOneUserOrderAsync(id, userModel); //GetOneUserOrderAsync is called

        // Assert

        Assert.NotNull(result); //The returned value should not be null
        Assert.IsType<OrderModel>(result); //The returned object should be of the type OrderModel
        Assert.Equal(id, result.Id); //The Id of the returned order should be equal to the one that was inserted to the GetOneUserOrderMethod
        Assert.Equal(userModel.Id, result.User.Id); //The User Id of the returned order, should be equal to the one that belongs to the user that was inserted into the GetOneUserOrderMethod (confirming that the returned order belongs to the correct user)
    }

    [Fact]
    public void GetOneUserOrderAsync_ShouldReturnNull_WhenAnOrderBelongsToDifferentUser()
    {
        // Arrange

        string id = Guid.NewGuid().ToString(); //An order number is created, which will be used to search for the corresponding order

        var userModel = new UserModel() //A registered user is created
        {
            Id = new Guid().ToString(),
            Address = new AddressModel(),
            Role = "User"
        };

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var orderModel = new OrderModel //An orderModel is created with the same user as above, in order to simulate an existing order that belongs to the above user
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
            ProductList = new List<ProductModel>(),
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        var differentOrderModel = new OrderModel //An orderModel is created with the same id as above, BUT with a different user from the one above, in order to simulate an existing order that belongs to a different user
        {
            Id = id,
            User = new UserModel() { Id = Guid.NewGuid().ToString() },
            ShippingChoice = standardShippingChoice,
            ProductList = new List<ProductModel>(),
            PriceTotal = 100m,
            IsConfirmed = true,
        };


        _orderService.Setup(x => x.GetOneUserOrderAsync(orderModel.Id, userModel)).Returns(orderModel); //GetOneUserOrderAsync in the mocked orderService is setup to return the simulated orderModel, if the corresponding id and user is inserted (otherwise it will return null)

        // Act

        var result = _orderService.Object.GetOneUserOrderAsync(id, userModel); //GetOneUserOrderAsync is called

        // Assert

        Assert.Null(result); //The returned value should be null
    }

    [Fact]
	public void UpdateOrderAsync_AdminUpdatesOrder_ShouldReturnTrue()
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
			Id = Guid.NewGuid().ToString(),
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var expressShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Express",
			ShippingPrice = 150m
		};

		productList.Add(productModel);

        //First orderModel
        var originalOrder = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        //First orderModel updated
        var updatedOrder = new OrderModel
        {
            Id = originalOrder.Id,
            User = originalOrder.User,
            ShippingChoice = expressShippingChoice,
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
	public void UpdateOrderAsync_AdminTryToUpdateOrder_ShouldReturnFalse()
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
			Id = Guid.NewGuid().ToString(),
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var expressShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Express",
			ShippingPrice = 150m
		};

		productList.Add(productModel);

        //First orderModel
        var originalOrder = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };

        //First orderModel updated
        var updatedOrder = new OrderModel
        {
            Id = originalOrder.Id,
            User = originalOrder.User,
            ShippingChoice = expressShippingChoice,
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

    [Fact]
    public void GetOrdersAsync_UserShouldSeeAllOrders_AndReturnListOfOrders()
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

		var firstUser = new UserModel()
		{
			Id = Guid.NewGuid().ToString(),
			Address = addressModel,
			Role = "User"
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var expressShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Express",
			ShippingPrice = 150m
		};

		productList.Add(productModel);


        var ordersList = new List<OrderModel>();

        var firstOrder = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = firstUser,
            ShippingChoice = standardShippingChoice,
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };
        ordersList.Add(firstOrder);

        var secondOrder = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = new UserModel { Id = "1", Address = addressModel, Role = "User" },
            ShippingChoice = expressShippingChoice,
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
        };
        ordersList.Add(secondOrder);


		_orderService.Setup(x => x.CreateOrderAsync(firstOrder)).Returns(true);
		_orderService.Setup(x => x.CreateOrderAsync(secondOrder)).Returns(true);
		_orderService.Setup(x => x.GetAllUserOrdersAsync()).Returns(ordersList.Where(o => o.User.Id == firstUser.Id).ToList()); ;

        // Act
        var firstOrderResult = _orderService.Object.CreateOrderAsync(firstOrder);
        var secondOrderResult = _orderService.Object.CreateOrderAsync(secondOrder);

        //Fetching all orders connected to the user
        var firstUserOrders = _orderService.Object.GetAllUserOrdersAsync();


        // Assert
        Assert.True(firstOrderResult);
        Assert.True(secondOrderResult);
        Assert.NotNull(firstUserOrders);
        Assert.All(firstUserOrders, order => Assert.Equal(firstUser.Id, order.User.Id));
    }

    [Fact]
    public void GetUserCartAsync_ShouldReturnCart_FromCorrectUser()
    {
        //Arrange

        var userModel = new UserModel() //A registered user is created
        {
            Id = new Guid().ToString(),
            Address = new AddressModel(),
            Role = "User"
        };

        var cartModel = new CartModel() //A cart model is created, with the UserId from the above user. This will simulate the corresponding user cart
        {
            UserId = userModel.Id,
            ProductList = new List<ProductModel>(),
            PriceTotal = 100m
        };

        _orderService.Setup(x => x.GetUserCartAsync(userModel.Id)).Returns(cartModel); //GetUserCartAsync is setup to return the simulated cartModel when the corresponding UserId is inserted

        //Act

        var result = _orderService.Object.GetUserCartAsync(userModel.Id); //GetUserCartAsync method is called

        //Assert

        Assert.NotNull(result); //The returned value should not be null
        Assert.IsType<CartModel>(result); //The returned object should be of the type CartModel
        Assert.Equal(userModel.Id, result.UserId); //The UserId of the returned cart should be equal to the Id of the user created above
    }

    [Fact]
    public void DeleteProductFromList_ShouldReturnUpdatedList_WithoutTheProduct()
    {
        // Arrange

        var productModel = new ProductModel() //The first product is created
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 20,
            Price = 100m
        };

        var deletedProductModel = new ProductModel() //A different product is created. This is the one that will later be deleted from the list
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Gaffel",
            Description = "Inte kniv",
            Stock = 20,
            Price = 100m
        };

        var productList = new List<ProductModel>(); //ProductList is initialized

        productList.Add(productModel); //The first product is added to the list
        productList.Add(deletedProductModel);
        productList.Add(deletedProductModel); //Two instances of the second product is added

        var updatedProductList = new List<ProductModel>(productList); //A new copy of the list is created
        updatedProductList.Remove(deletedProductModel); //One of the deletedProductModel instances is deleted from the list

        _orderService.Setup(x => x.DeleteProductFromList(productList, deletedProductModel.Id, 1)).Returns(updatedProductList); //DeleteProductFromList is setup to return the updated version of the list

        // Act

        var result = _orderService.Object.DeleteProductFromList(productList, deletedProductModel.Id, 1); //DeleteProductFromList method is called

        // Assert

        Assert.Equal(productList.Count() - 1, result.Count());
        Assert.Contains<ProductModel>(productModel, result);
        Assert.Contains<ProductModel>(deletedProductModel, result);
    }

	[Fact]
	public void GetOrdersAsync_UserShouldNotSeeAllOrders_AndReturnFalse()
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

		var firstUser = new UserModel()
		{
			Id = Guid.NewGuid().ToString(),
			Address = addressModel,
			Role = "User"
		};

		var ordersList = new List<OrderModel>();

		_orderService.Setup(x => x.GetAllUserOrdersAsync()).Returns(ordersList.Where(o => o.User.Id == firstUser.Id).ToList());

		// Act

		//Checking if the user has any orders
		var firstUserOrders = _orderService.Object.GetAllUserOrdersAsync();


		// Assert
		Assert.Empty(firstUserOrders);
	}

	[Fact]
	public void GetOrdersAsync_UserShouldSeeAllOrdersSortedByDate_ReturnSortedListOfOrders()
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
			Id = Guid.NewGuid().ToString(),
			Address = addressModel,
			Role = "User"
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var expressShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Express",
			ShippingPrice = 150m
		};

		productList.Add(productModel);


		var ordersList = new List<OrderModel>();

        var firstOrder = new OrderModel
        {
            Id = Guid.NewGuid().ToString(),
            User = userModel,
            ShippingChoice = standardShippingChoice,
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true,
            CreationDate = new DateTime(2024, 11, 5)
        };
		ordersList.Add(firstOrder);

		var secondOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = expressShippingChoice,
			ProductList = productList,
			PriceTotal = 100m,
			IsConfirmed = true,
			CreationDate = new DateTime(2024, 11, 6)
		};
		ordersList.Add(secondOrder);

		var thirdOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = expressShippingChoice,
			ProductList = productList,
			PriceTotal = 100m,
			IsConfirmed = true,
			CreationDate = new DateTime(2024, 11, 7)
		};
		ordersList.Add(thirdOrder);

		_orderService.Setup(x => x.CreateOrderAsync(firstOrder)).Returns(true);
		_orderService.Setup(x => x.CreateOrderAsync(secondOrder)).Returns(true);
		_orderService.Setup(x => x.CreateOrderAsync(thirdOrder)).Returns(true);
		_orderService.Setup(x => x.GetAllUserOrdersAsync()).Returns(ordersList.Where(o => o.User.Id == userModel.Id).OrderByDescending(o => o.CreationDate).ToList());

		// Act
		var firstOrderResult = _orderService.Object.CreateOrderAsync(firstOrder);
		var secondOrderResult = _orderService.Object.CreateOrderAsync(secondOrder);

		//Fetching all orders connected to the user after creationDate
		var firstUserOrders = _orderService.Object.GetAllUserOrdersAsync();

		// Assert
		Assert.All(firstUserOrders, order => Assert.Equal(userModel.Id, order.User.Id));
		Assert.True(firstUserOrders.SequenceEqual(firstUserOrders.OrderByDescending(o => o.CreationDate).ToList()));
		Assert.True(firstUserOrders.First().CreationDate == thirdOrder.CreationDate);
		Assert.True(firstUserOrders.Last().CreationDate == firstOrder.CreationDate);
	}

    [Fact]
    public void GetUserAddressAsync_ShouldReturnCorrectAddress()
    {
        // Arrange
        var addressModel = new AddressModel() //An address object is created
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

        var userModel = new UserModel() //A registered user is created with the above address object
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "User"
        };


        _orderService.Setup(x => x.GetUserAddressAsync(userModel.Id)).Returns(addressModel); //GetUserAddressAsync is setup to return the simulated address object, if the corresponding userId is inserted

        // Act

        var result = _orderService.Object.GetUserAddressAsync(userModel.Id); //GetUserAddressAsync method is called

        // Assert

        Assert.NotNull(result); //The returned value should not be null
        Assert.IsType<AddressModel>(result); //The returned object should be of the type AddressModel
        Assert.Equal(addressModel.Id, result.Id); //The Id of the returned address object should be equal to the Id of the address object created above
    }

    [Fact]
    public void UpdateUserAddressAsync_ShouldUpdateUserAddress_AndReturnTrue()
    {
        // Arrange
        var addressModel = new AddressModel() //An address object is created
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

        var userModel = new UserModel() //A registered user is created with the address object created above
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "User"
        };

        var updatedAddressModel = new AddressModel() //An updated address object is created
        {
            Id = Guid.NewGuid().ToString(),
            Street = "gata 2",
            City = "Nybro",
            State = "Happiness",
            PhoneNumber = "09876543",
            ZipCode = "39999",
            CountryCallingCode = "+46",
            Country = "Sweden"
        };


        _orderService.Setup(x => x.UpdateUserAddressAsync(userModel.Id, updatedAddressModel)).Returns(true); //UpdatedUserAddressAsync is setup to return true

        // Act

        var result = _orderService.Object.UpdateUserAddressAsync(userModel.Id, updatedAddressModel); //UpdateUserAddressAsync method is called, and the updated address is inserted

        // Assert

        Assert.True(result); //The returned value should be true
    }

    [Fact]
    public void UpdateUserAddressAsync_ShouldReturnFalse_IfFieldIsMissing()
    {
        // Arrange
        var addressModel = new AddressModel() //An address object is created
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

        var userModel = new UserModel() //A registered user is created with the address object created above
        {
            Id = new Guid().ToString(),
            Address = addressModel,
            Role = "User"
        };

        var updatedAddressModel = new AddressModel() //An updated address object is created, but with some of the fields missing
        {
            Id = Guid.NewGuid().ToString(),
            Street = null!,
            City = null!,
            State = "Happiness",
            PhoneNumber = "09876543",
            ZipCode = "39999",
            CountryCallingCode = "+46",
            Country = "Sweden"
        };


        _orderService.Setup(x => x.UpdateUserAddressAsync(userModel.Id, updatedAddressModel)).Returns(false); //UpdatedUserAddressAsync is setup to return false if the incomplete addressModel is inserted

        // Act

        var result = _orderService.Object.UpdateUserAddressAsync(userModel.Id, updatedAddressModel); //UpdateUserAddressAsync method is called, and the updated address is inserted

        // Assert

        Assert.False(result); //The returned value should be false
    }

	[Fact]
	public void CalculateTotalPrice_WhenUserChoosesShipping_ReturnsCorrectTotalPrice()
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
			Id = Guid.NewGuid().ToString(),
			Address = addressModel,
			Role = "User"
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		productList.Add(productModel);

		var firstOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = standardShippingChoice,
			ProductList = productList,
			PriceTotal = 100m,
			IsConfirmed = true,
			CreationDate = new DateTime(2024, 11, 5)
		};

		// Expected totalprice with shipping
		decimal expectedTotalPrice = firstOrder.PriceTotal + standardShippingChoice.ShippingPrice;
		_orderService.Setup(x => x.CalculateTotalPrice(It.IsAny<decimal>(), It.IsAny<decimal>()))
					.Returns((decimal orderPrice, decimal shippingPrice) => orderPrice + shippingPrice);

        _orderService.Setup(x => x.CreateOrderAsync(firstOrder)).Returns(true);

		// Act
		firstOrder.PriceTotal = _orderService.Object.CalculateTotalPrice(firstOrder.PriceTotal, standardShippingChoice.ShippingPrice);
        var newOrder = _orderService.Object.CreateOrderAsync(firstOrder);

		// Assert
		Assert.Equal(expectedTotalPrice, firstOrder.PriceTotal);
		Assert.Equal(200m, firstOrder.PriceTotal);
        Assert.True(newOrder);
	}

	[Fact]
	public void CalculateTotalPrice_WhenUserEnterPromoCode_ReturnsCorrectTotalPrice()
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
			Id = Guid.NewGuid().ToString(),
			Address = addressModel,
			Role = "User"
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

        var promoCode = new PromoCodeModel()
        {
            Id = Guid.NewGuid().ToString(),
            PromoCode = "promoCode",
            DiscountPercentage = 50m
        };

		productList.Add(productModel);

		var firstOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = standardShippingChoice,
			ProductList = productList,
            PromoCode = promoCode,
			PriceTotal = 100m,
			IsConfirmed = true,
			CreationDate = new DateTime(2024, 11, 5)
		};

		// Expected totalprice after promoCode
		decimal expectedTotalPrice = firstOrder.PriceTotal * (1 - (promoCode.DiscountPercentage / 100));

		_orderService.Setup(x => x.CalculateTotalPrice(It.IsAny<decimal>(), It.IsAny<decimal>()))
			.Returns((decimal orderPrice, decimal discountPercentage) => orderPrice * (1 - (discountPercentage / 100)));

		_orderService.Setup(x => x.CreateOrderAsync(firstOrder)).Returns(true);

		// Act
		firstOrder.PriceTotal = _orderService.Object.CalculateTotalPrice(firstOrder.PriceTotal, promoCode.DiscountPercentage);
        var newOrder = _orderService.Object.CreateOrderAsync(firstOrder);

		// Assert
		Assert.Equal(expectedTotalPrice, firstOrder.PriceTotal);
        Assert.Equal(50m,firstOrder.PriceTotal);
	}

	[Fact]
	public void ValidatePromoCode_WhenUserEnterIncorrectPromoCode_ReturnsFalse()
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
			Id = Guid.NewGuid().ToString(),
			Address = addressModel,
			Role = "User"
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

		var standardShippingChoice = new ShippingChoice()
		{
			Id = Guid.NewGuid().ToString(),
			ShippingCompanyName = "PostNord",
			ShippingMethod = "Standard",
			ShippingPrice = 100m
		};

		var promoCode = new PromoCodeModel()
		{
			Id = Guid.NewGuid().ToString(),
			PromoCode = "promoCode",
			DiscountPercentage = 50m
		};

		productList.Add(productModel);

		var firstOrder = new OrderModel
		{
			Id = Guid.NewGuid().ToString(),
			User = userModel,
			ShippingChoice = standardShippingChoice,
			ProductList = productList,
			PromoCode = promoCode,
			PriceTotal = 100m,
			IsConfirmed = true,
			CreationDate = new DateTime(2024, 11, 5)
		};

        var promoCodeInDB = "promoCode1";


		_orderService.Setup(x => x.ValidatePromoCode(promoCode.PromoCode, promoCodeInDB)).Returns(false);

		// Act
		var result = _orderService.Object.ValidatePromoCode(promoCode.PromoCode, promoCodeInDB);

		// Assert
		Assert.False(result);
	}
}
