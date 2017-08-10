namespace CSharpExtensionMethodDemo
{
    public class Order
    {
    }

    public class VipOrder
    {
    }

    public class OrderHelper
    {
        public static VipOrder ConvertToVipOrder(Order order)
        {
            return new VipOrder();
        }
    }
}