namespace DistributeExtensionPatternDemo
{
    using System;

    public static class ExtensionMethods
    {
        public static void DistributeVipOrder(this IDistribute<IOrder> distribute, IOrder vipOrder)
        {
            Console.WriteLine("DistributeVipOrder");
        }
    }
}