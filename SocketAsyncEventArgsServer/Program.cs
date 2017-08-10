namespace SocketAsyncEventArgsServer
{
    using System;
    using System.Net;

    using SocketAsyncEventArgsServer.Helper;

    class Program
    {
        private static int BufferSize = Convert.ToInt32(
            System.Configuration.ConfigurationManager.AppSettings["BufferSize"].ToString());

        private static int ListenPort = Convert.ToInt32(
            System.Configuration.ConfigurationManager.AppSettings["ListenPort"].ToString());

        private static int MaxClient = Convert.ToInt32(
            System.Configuration.ConfigurationManager.AppSettings["MaxClient"].ToString());

        static void Main(string[] args)
        {
            LogHelper.Log.InfoFormat("等待客户端连接");
            Server server = new Server(MaxClient, BufferSize);
            server.Init();
            server.Start(new IPEndPoint(IPAddress.Any, ListenPort));
            Console.WriteLine("按任意键结束服务程序.");
            Console.ReadKey();
        }
    }
}