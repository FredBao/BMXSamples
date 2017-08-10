namespace CSharpExtensionMethodDemo
{
    public static class ExtensionMethods
    {
        public static VipOrder ConvertToVipOrder(this Order order)
        {
            return new VipOrder();
        }
    }
}