using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace OrderProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderRepository _repo;
        public OrderController(OrderRepository repo)
        {
            _repo = repo;

        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder(CreateOrderModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!order.PaymentIsConfirmed)
                    {
                        return BadRequest("The payment for this order has not been confirmed.");
                    }

                    decimal priceTotal = 0m;

                    foreach (var product in order.ProductList)
                    {
                        if (product.Stock < 1)
                        {
                            return BadRequest("One or more of the provided products are not in stock");
                        }

                        priceTotal += product.Price;
                    }

                    priceTotal += order.ShippingChoice.ShippingPrice;

                    var productListJSON = JsonConvert.SerializeObject(order.ProductList);

                    var entity = new OrderEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = order.UserId,
                        Address = order.DeliveryAddress,
                        DeliveryAddress = order.DeliveryAddress,
                        ShippingChoice = $"{order.ShippingChoice.ShippingCompanyName} {order.ShippingChoice.ShippingMethod}",
                        ProductList = productListJSON,
                        PriceTotal = priceTotal,
                    };

                    var result = await _repo.CreateAsync(entity);

                    if (result)
                    {
                        return CreatedAtAction("CreateOrder", entity);
                    }

                    return BadRequest("Error while adding order to database");
                }

                return BadRequest(ModelState);

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
