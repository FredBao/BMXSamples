namespace PassivationPatternDemo
{
    using System;
    using System.Linq;

    [Serializable]
    internal class InfoMationer
    {
        internal bool CheckNumber(Order order, ref OrderExamineApproveManagerHandler handler)
        {
            Console.WriteLine("CheckNumber");
            if (order.OrderItems.Any(s => s.Number <= 0))
            {
                handler -= this.CheckNumber;
                return false;
            }
            else
            {
                return true;
            }
        }

        internal bool CheckPrice(Order order, ref OrderExamineApproveManagerHandler handler)
        {
            Console.WriteLine("CheckPrice");
            if (order.OrderItems.Any(s => s.Product.Price <= 0))
            {
                handler -= this.CheckPrice;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}