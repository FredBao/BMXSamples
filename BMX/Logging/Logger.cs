namespace Bmx.Abp.Logging
{
    using System;

    using Bmx.Abp.Configuration;

    using Infrastructure;

    using log4net;

    public class Logger : ILogger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Debug(string content)
        {
            Log.Debug(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }

        public void Info(string content)
        {
            Log.Info(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }

        public void Warn(string content)
        {
            Log.Warn(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }

        public void Error(string content)
        {
            Log.Error(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }
    }
}
