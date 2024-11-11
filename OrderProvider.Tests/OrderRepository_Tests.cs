using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace OrderProvider.Tests;

public class OrderRepository_Tests
{
	//InMemoryDatabase is initialized

	private readonly DataContext _context =
		new(new DbContextOptionsBuilder<DataContext>()
			.UseInMemoryDatabase($"{Guid.NewGuid()}")
			.Options);                                      

    private OrderRepository _orderRepository;
	private AddressModel _addressModel;
	private UserModel _userModel;
	private List<ProductModel> _productList = new List<ProductModel>();
	private ProductModel _productModel;

	public OrderRepository_Tests()
	{
		_orderRepository = new OrderRepository(_context);

		_addressModel = new AddressModel()
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

		_userModel = new UserModel()
		{
			Id = new Guid().ToString(),
			Address = _addressModel,
			Role = "Admin"
		};

		_productModel = new ProductModel()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stövel",
            Description = "Bootstrap Bill",
            Stock = 20,
            Price = 100m
        };

		_productList.Add(_productModel);
    }

    [Fact]

	public async Task CreateAsync_ShouldCreateAnOrderEntityInDatabase_And_ReturnTrue()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity 
		{ 
			Id = Guid.NewGuid().ToString(),
            UserId = _userModel.Id,
            Address = addressJSON,
            ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		// Act

		var result = await _orderRepository.CreateAsync(orderEntity);

		// Assert

		Assert.True(result);
	}

    [Fact]

    public async Task CreateAsync_ShouldNotCreateAnOrderEntityInDatabase_And_ReturnFalse()
    {

        // Arrange

        var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
        var productListJSON = JsonConvert.SerializeObject(_productList);

        var orderEntity = new OrderEntity
        {
            Id = null!,
            Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
            ProductList = productListJSON,
            PriceTotal = 100m
        };

        // Act

        var result = await _orderRepository.CreateAsync(orderEntity);

        // Assert

        Assert.True(!result);
    }

	[Fact]

	public async Task GetOneAsync_ShouldGetOneOrderEntityInDatabase_And_ReturnOrderEntity()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity
		{
			Id = Guid.NewGuid().ToString(),
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};
		var order = await _orderRepository.CreateAsync(orderEntity);


		// Act
		var existingOrder = await _orderRepository.GetOneAsync(x => x.Equals(orderEntity));
		

		// Assert
		Assert.NotNull(existingOrder);
	}

	[Fact]

	public async Task GetOneAsync_ShouldNotGetOneOrderEntityInDatabase_And_ReturnNull()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity
		{
			Id = Guid.NewGuid().ToString(),
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		// Act
		var existingOrder = await _orderRepository.GetOneAsync(x => x.Equals(orderEntity));


		// Assert
		Assert.Null(existingOrder);
	}

	[Fact]

	public async Task GetAllAsync_ShouldGetAllOrderEntitiesInDatabase_And_ReturnListOfOrderEntity()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity
		{
			Id = Guid.NewGuid().ToString(),
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		var order = await _orderRepository.CreateAsync(orderEntity);


		// Act
		var existingOrders = await _orderRepository.GetAllAsync();


		// Assert
		Assert.NotNull(existingOrders);
	}

	[Fact]

	public async Task GetAllAsync_ShouldGetAllOrderEntitiesInDatabase_And_ReturnNull()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity
		{
			Id = Guid.NewGuid().ToString(),
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		// Act
		var existingOrders = await _orderRepository.GetAllAsync();


		// Assert
		Assert.Empty(existingOrders);
	}

	[Fact]

	public async Task UpdateAsync_ShouldUpdateOneOrderEntitieInDatabase_And_ReturnTrue()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var oldOrder = new OrderEntity
		{
			Id = "1",
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};
		var newOrder = new OrderEntity
		{
			Id = "1",
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Express",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		await _orderRepository.CreateAsync(oldOrder);

		// Act
		var updatedOrder = await _orderRepository.UpdateAsync(x => x.Id == oldOrder.Id, newOrder);


		// Assert
		Assert.True(updatedOrder);
	}

	[Fact]

	public async Task UpdateAsync_ShouldNotUpdateOneOrderEntitieInDatabase_And_ReturnFalse()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var oldOrder = new OrderEntity
		{
			Id = "1",
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};
		var newOrder = new OrderEntity
		{
			Id = "2",
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Express",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		await _orderRepository.CreateAsync(oldOrder);

		// Act
		var updatedOrder = await _orderRepository.UpdateAsync(x => x.Id == oldOrder.Id, newOrder);


		// Assert
		Assert.False(updatedOrder);
	}


	[Fact]
	public async Task DeleteAsync_ShouldDeleteAnOrderEntityInDatabase_And_ReturnTrue()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity
		{
			Id = "1",
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		// Act
		var createResult = await _orderRepository.CreateAsync(orderEntity);
		var existingOrder = await _orderRepository.GetOneAsync(x => x.Id == orderEntity.Id);
		var deleteResult = await _orderRepository.DeleteAsync(x => x.Id == existingOrder.Id);


		// Assert

		Assert.True(createResult);
		Assert.True(deleteResult);
		Assert.NotNull(existingOrder);
	}

	[Fact]
	public async Task DeleteAsync_ShouldNotDeleteAnOrderEntityInDatabase_And_ReturnFalse()
	{

		// Arrange

		var addressJSON = JsonConvert.SerializeObject(_userModel.Address);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity
		{
			Id = "1",
			Address = addressJSON,
			UserId = _userModel.Id,
			ShippingChoice = "PostNord Standard",
			ProductList = productListJSON,
			PriceTotal = 100m
		};

		// Act
		var createResult = await _orderRepository.CreateAsync(orderEntity);
		var existingOrder = await _orderRepository.GetOneAsync(x => x.Id == orderEntity.Id);
		var deleteResult = await _orderRepository.DeleteAsync(x => x.Id == existingOrder.Id);

		// Assert

		Assert.True(createResult);
		Assert.False(!deleteResult);
		Assert.NotNull(existingOrder);
	}
}
