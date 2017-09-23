using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClient
{
    using WCFClient.ServiceReference1;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CutterServiceClient client = new ServiceReference1.CutterServiceClient();

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

                Console.WriteLine(cutterCompensationMax);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadKey();
        }
    }
}
