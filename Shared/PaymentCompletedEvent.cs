namespace Shared
{
    public class PaymentCompletedEvent
    {
        public string OrderId{ get; set; }
        public string BuyerId{ get; set; }
    }
}
