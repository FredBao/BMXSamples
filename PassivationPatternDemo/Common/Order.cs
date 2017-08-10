namespace PassivationPatternDemo
{
    using System.Collections.Generic;

    public class Order
    {
        public Customer Customer { get; set; }

        public string OrderId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}