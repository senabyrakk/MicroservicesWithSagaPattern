using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using Stock.Api.Model;
using System;
using System.Threading.Tasks;

namespace Stock.Api.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailEvent>
    {
        private readonly AppDbContext _context;
        private ILogger<PaymentFailedEventConsumer> _logger;

        public PaymentFailedEventConsumer(AppDbContext orderDbContext, ILogger<PaymentFailedEventConsumer> logger)
        {
            _context = orderDbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailEvent> context)
        {
            foreach (var item in context.Message.OrderItemMessage)
            {
                var stock = await _context.Stock.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (stock != null)
                {
                    stock.Count += item.Count;
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("payment failed. Stock updated");
        }
    }
}
