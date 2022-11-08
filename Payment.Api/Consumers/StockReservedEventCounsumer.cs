using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;
using System.Threading.Tasks;

namespace Payment.Api.Consumers
{
    public class StockReservedEventCounsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEventCounsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventCounsumer(ILogger<StockReservedEventCounsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var balance = 100m;
            if (balance > context.Message.Payment.TotalPrice)
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was withdrawn from creditcard for user id : {context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentCompletedEvent() {BuyerId = context.Message.BuyerId,OrderId = context.Message.OrderId });
            }
            else
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} tl was not withdrawn from credit card for user id : {context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentFailEvent() { BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId,Message = "not enough balance" ,OrderItemMessage = context.Message.OrderItems});
            }
        }
    }
}
