using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Data;
using Order.Api.DTOs;
using Order.Api.Models;
using Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext orderDbContext;
        private readonly IPublishEndpoint publishEndpoint;

        public OrdersController(OrderDbContext orderDbContext, IPublishEndpoint publishEndpoint)
        {
            this.orderDbContext = orderDbContext;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateDto orderCreateDto)
        {
            var newOrder = new Models.Order
            {
                BuyerId = orderCreateDto.BuyerId,
                Status = OrderStatus.Suspend,
                Address = new Address()
                {
                    Line = orderCreateDto.Address.Line,
                    District = orderCreateDto.Address.District,
                    Province = orderCreateDto.Address.Province
                },
                CreatedDate = System.DateTime.Now
            };

            orderCreateDto.OrderItems.ForEach(item =>
            {
                newOrder.Items.Add(new OrderItem()
                {
                    Price = item.Price,
                    Count = item.Count,
                    ProductId = item.ProductId,
                });
            });

            await orderDbContext.Order.AddAsync(newOrder);
            await orderDbContext.SaveChangesAsync();

            var orderCreateEvent = new OrderCreatedEvent
            {
                BuyerId = newOrder.BuyerId,
                OrderId = newOrder.Id.ToString(),
                Payment = new PaymentMessage
                {
                    CardName = orderCreateDto.Payment.CardName,
                    CardNumber = orderCreateDto.Payment.CardNumber,
                    Expiration = orderCreateDto.Payment.Expiration,
                    CVV = orderCreateDto.Payment.CVV,
                    TotalPrice = orderCreateDto.OrderItems.Sum(x => x.Price * x.Count)
                },
            };

            orderCreateDto.OrderItems.ForEach(item =>
            {
                orderCreateEvent.orderItems.Add(new OrderItemMessage() {
                Count = item.Count,
                ProductId = item.ProductId
                });
            });

            await publishEndpoint.Publish(orderCreateEvent);
            return Ok();

        }
    }
}
