using MassTransit;
using Microsoft.Extensions.Logging;
using Order.Api.Data;
using Order.Api.Models;
using Shared;
using System;
using System.Threading.Tasks;

namespace Order.Api.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailEvent>
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ILogger<PaymentCompletedEventConsumer> _logger;

        public PaymentFailedEventConsumer(OrderDbContext orderDbContext, ILogger<PaymentCompletedEventConsumer> logger)
        {
            _orderDbContext = orderDbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailEvent> context)
        {
            var order = await _orderDbContext.Order.FindAsync(Int32.Parse(context.Message.OrderId));
            if (order != null)
            {
                order.Status = OrderStatus.Fail;
                order.FailMessage = context.Message.Message;
                await _orderDbContext.SaveChangesAsync();

                _logger.LogInformation($"order id : {order.Id} status change. Status : {order.Status}");
            }
            else
            {
                _logger.LogError($"order not found. id :{context.Message.OrderId}");
            }

        }
    }
}
