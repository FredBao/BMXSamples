namespace PassivationPatternDemo
{
    using System;

    public delegate bool OrderExamineApproveManagerHandler(Order order, ref OrderExamineApproveManagerHandler handler);

    [Serializable]
    public class OrderExamineApproveManager
    {
        private OrderExamineApproveManagerHandler Flows;

        public static OrderExamineApproveManager CreateFlow()
        {
            OrderExamineApproveManager manager = new OrderExamineApproveManager();

            InfoMationer infoMationer = new InfoMationer();
            manager.Flows += infoMationer.CheckPrice;
            manager.Flows += infoMationer.CheckNumber;

            BusinessManager businessManager = new BusinessManager();
            manager.Flows += businessManager.CallPhoneConfirm;
            manager.Flows += businessManager.SendEmailConfim;

            GeneralManager generalManager = new GeneralManager();
            manager.Flows += generalManager.FinalConfirm;
            manager.Flows += generalManager.SendEmailConfim;

            return manager;
        }

        public void RunFlows(Order order)
        {
            this.Flows(order, ref this.Flows);
        }
    }
}