using System.Collections.Generic;

namespace Shared
{
    public class PaymentFailEvent
    {
        public string OrderId { get; set; }
        public string BuyerId { get; set; }
        public string Message{ get; set; }
        public List<OrderItemMessage> OrderItemMessage { get; set; } = new List<OrderItemMessage>();
    }
}
