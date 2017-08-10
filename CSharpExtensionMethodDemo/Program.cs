namespace CSharpExtensionMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();

            // 把Order类型转换成VipOrder
            // 1.未使用扩展方法
            VipOrder vipOrder = OrderHelper.ConvertToVipOrder(order);

            // 2.使用扩展方法
            VipOrder vipOrder2 = order.ConvertToVipOrder();
        }
    }
}