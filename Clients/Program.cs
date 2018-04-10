using Contracts;
using System;
using System.Net;
using System.ServiceModel;

namespace Clients
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorService");
            //NetworkCredential credential = channelFactory.Credentials.Windows.ClientCredential;
            //ICalculator calculator = channelFactory.CreateChannel();
            //Invoke(calculator);
            NetworkCredential credential = channelFactory.Credentials.Windows.ClientCredential;
            credential.UserName = "fred";
            credential.Password = "bmxBMX123!@#";
            //channelFactory = new ChannelFactory<ICalculator>("calculatorService");
            ICalculator calculator = channelFactory.CreateChannel();

           

            Invoke(calculator);

            Console.Read();
        }

        static void Invoke(ICalculator calculator)
        {
            try
            {
                calculator.Add(1, 2);
                Console.WriteLine("服务调用成功...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"服务调用失败...{ex.ToString()}");
            }
        }
    }
}
