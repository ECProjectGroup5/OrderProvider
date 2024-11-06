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

    public async Task CreateOrderAsync_ShouldCreateAnOrderEntityInDatabase_And_ReturnTrue()
    {

    }

}
