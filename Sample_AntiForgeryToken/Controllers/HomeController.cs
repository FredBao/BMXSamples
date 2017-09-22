namespace Sample_AntiForgeryToken.Controllers
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    using FanucToolManageDll;

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
        public ActionResult Text(string notice)
        {
            try
            {
                var ftm = new FanucToolManageDll(50);

                uint cutterCompensationMax;

                if (ftm.GetThresholdValue2(out cutterCompensationMax))
                {
                    notice = $"输入最大上限{cutterCompensationMax},成功";
                }
                else
                {
                    notice = $"输入最大上限{cutterCompensationMax},失败";
                }

                ftm.Disconnect();
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