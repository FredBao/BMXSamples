using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFSelfHostServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Sample_WcfServiceLibrary.Service1)))
            {
                host.Open();
                Console.WriteLine("Please input exit to close host.");
                Console.ReadKey();
            }
        }
    }
}
