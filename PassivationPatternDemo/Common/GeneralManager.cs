namespace PassivationPatternDemo
{
    using System;

    [Serializable]
    internal class GeneralManager
    {
        public GeneralManager()
        {
        }

        internal bool FinalConfirm(Order order, ref OrderExamineApproveManagerHandler handler)
        {
            Console.WriteLine("FinalConfirm");
            if (order.Customer.Name == "12345")
            {
                handler -= this.FinalConfirm;
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
                return true;
            }

            return false;
        }
    }
}