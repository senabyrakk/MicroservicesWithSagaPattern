using System.Collections.Generic;

namespace Shared
{
    public class OrderCreatedEvent
    {
        public string OrderId{ get; set; }
        public string BuyerId { get; set; }
        public PaymentMessage Payment{ get; set; }
        public List<OrderItemMessage> orderItems { get; set; } = new List<OrderItemMessage>();
    }
}
