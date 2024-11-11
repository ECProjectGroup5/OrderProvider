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
                if (ModelState.IsValid) //The following code will only be executed if the ModelState is valid, otherwise a BadRequest will be returned
                {
                    if (!order.PaymentIsConfirmed) //Check if the payment has been confirmed for the order
                    {
                        return BadRequest("The payment for this order has not been confirmed.");
                    }

                    decimal priceTotal = 0m; //A variable for the total price of the order is initialized, so that it can be modified later

                    foreach (var product in order.ProductList)
                    {
                        if (product.Stock < 1) //Every product in the product list is checked to see if they're out of stock
                        {
                            return BadRequest("One or more of the provided products are not in stock");
                        }

                        priceTotal += product.Price; //The price of each product is then added to the total price
                    }

                    priceTotal += order.ShippingChoice.ShippingPrice; //The shipping price is added to the total price

                    var productListJSON = JsonConvert.SerializeObject(order.ProductList); //The product list is converted to JSON, so that it can be stored in the order entity

                    var entity = new OrderEntity() //The order entity is created, with the values provided in the CreateOrderModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = order.UserId,
                        Address = order.DeliveryAddress,
                        DeliveryAddress = order.DeliveryAddress,
                        ShippingChoice = $"{order.ShippingChoice.ShippingCompanyName} {order.ShippingChoice.ShippingMethod}",
                        ProductList = productListJSON,
                        PriceTotal = priceTotal,
                    };

                    var result = await _repo.CreateAsync(entity); //The entity is added to the database

                    if (result)
                    {
                        return CreatedAtAction("CreateOrder", entity);
                    }

                    return BadRequest("Error while adding order to database");
                }

                return BadRequest(ModelState); //As previously mentioned, if the ModelState is invalid, a BadRequest will be returned along with the ModelState

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
