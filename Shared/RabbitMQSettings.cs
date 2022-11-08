namespace Shared
{
    public static class RabbitMQSettings
    {
        public const string StockOrderCreatedEventQueueName = "stock_order_created-queue";
        public const string StockReservedEventQueueName = "stock_reserved-queue";
        public const string StockPaymentFailEventQueueName = "Stock_payment_fail-queue";
        public const string OrderPaymentCompletedEventQueueName = "order_payment_completed-queue";
        public const string OrderPaymentFailedEventQueueName = "order_payment_failed-queue";
        public const string OrderStockNotReversedEventQueueName = "order_stock_not_reserved-queue";
    }
}
