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

		var addressJSON = JsonConvert.SerializeObject(_addressModel);
		var productListJSON = JsonConvert.SerializeObject(_productList);

		var orderEntity = new OrderEntity 
		{ 
			Id = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
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

        var addressJSON = JsonConvert.SerializeObject(_addressModel);
        var productListJSON = JsonConvert.SerializeObject(_productList);

        var orderEntity = new OrderEntity
        {
            Id = null!,
            Address = addressJSON,
            UserId = Guid.NewGuid().ToString(),
            ShippingChoice = "PostNord Standard",
            ProductList = productListJSON,
            PriceTotal = 100m
        };

        // Act

        var result = await _orderRepository.CreateAsync(orderEntity);

        // Assert

        Assert.True(!result);
    }
}
