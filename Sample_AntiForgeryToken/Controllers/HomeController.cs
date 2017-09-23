namespace Sample_AntiForgeryToken.Controllers
{
    using System;
    using System.Web.Mvc;

    using ServiceReference1;

    public class HomeController : Controller
    {
        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }

        public ActionResult Index()
        {

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Text2(string notice)
        {
            try
            {
                CutterServiceClient client = new CutterServiceClient();

                notice += client.GetThresholdValue2(111);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            this.ViewBag.Notice = notice;

            return this.View("Contact");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Text(string notice)
        {
            try
            {
                CutterServiceClient client = new CutterServiceClient();

                uint cutterCompensationMax = client.GetThresholdValue(
                    new ConnectedInfo()
                    {
                        IP = "10.10.100.39",
                        Port = 8193,
                        TimeOut = 3000,
                        MachineSystemType = 1,
                        CutterCompensationSide = 1,
                        FanucCutterCompensationArrayLength = 50,
                        FanucCutterCompensationAddress = 5013,

                    });

                notice += cutterCompensationMax;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            this.ViewBag.Notice = notice;

            return this.View("Contact");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Text3(string notice)
        {
            try
            {
                CutterServiceClient client = new CutterServiceClient();

                uint cutterCompensationMax = client.GetThresholdValue3(
                    new ConnectedInfo()
                    {
                        IP = "10.10.100.39",
                        Port = 8193,
                        TimeOut = 3000,
                        MachineSystemType = 1,
                        CutterCompensationSide = 1,
                        FanucCutterCompensationArrayLength = 50,
                        FanucCutterCompensationAddress = 5013,

                    });

                notice += cutterCompensationMax;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            this.ViewBag.Notice = notice;

            return this.View("Contact");
        }
    }
}