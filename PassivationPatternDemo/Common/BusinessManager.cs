namespace PassivationPatternDemo
{
    using System;
    using System.Threading;

    [Serializable]
    internal class BusinessManager
    {
        internal bool CallPhoneConfirm(Order order, ref OrderExamineApproveManagerHandler handler)
        {
            Console.WriteLine("CallPhoneConfirm");
            if (order.Customer.Name == "12345")
            {
                handler -= this.CallPhoneConfirm;
                return true;
            }

            return false;
        }

        internal bool SendEmailConfim(Order order, ref OrderExamineApproveManagerHandler handler)
        {
            Console.WriteLine("SendEmailConfim");
            if (order.Customer.Name == "12345")
            {
                handler -= this.SendEmailConfim;

                Thread.Sleep(5000);
                return true;
            }

            return false;
        }
    }
}