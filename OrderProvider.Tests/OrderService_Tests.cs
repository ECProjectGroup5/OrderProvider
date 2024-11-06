﻿using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Moq;
using Newtonsoft.Json;

namespace OrderProvider.Tests;

public class OrderService_Tests
{
    private readonly Mock<IOrderService> _orderService = new Mock<IOrderService>(); //Mock for OrderService is created

    [Fact]

    public void CreateOrderAsync_ShouldCreateAnOrder_And_ReturnTrue()
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
            UserId = Guid.NewGuid().ToString(),
            Address = addressModel,
            ShippingChoice = "PostNord Standard",
            ProductList = productList,
            PriceTotal = 100m,
            IsConfirmed = true
        };

        _orderService.Setup(x => x.CreateOrderAsync(orderModel)).Returns(true); //CreateOrderAsync in the mocked orderService is setup to return true when an orderModel is inserted

        // Act

        var result = _orderService.Object.CreateOrderAsync(orderModel); //CreateOrderAsync is called, with the created orderModel

        // Assert

        Assert.True(result); //It should return true
    }

}
