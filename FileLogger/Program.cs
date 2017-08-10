namespace FileLogger
{
    using System.Diagnostics;
    using System.Threading;

    using log4net;

    class Program
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                log.Info("这是测试信息");
                log.Debug("这是测试信息");
                Debug.WriteLine("这是测试信息");
            }
        }
    }
}