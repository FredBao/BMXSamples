namespace DistributeExtensionPatternDemo
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();
            VipOrder vipOrder = new VipOrder();
            IDistribute<IOrder> distribute = new OrderDistribute();
            distribute.DistributeOrder(order);
            distribute.DistributeOrder(vipOrder);
            distribute.DistributeVipOrder(vipOrder);
            Console.ReadKey();
        }
    }
}