namespace DistributeExtensionPatternDemo
{
    using System;

    public class OrderDistribute : IDistribute<IOrder>
    {
        public void DistributeOrder(IOrder t)
        {
            Console.WriteLine("Distribute" + t.GetType().ToString());
        }
    }
}