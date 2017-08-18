namespace BMX.Logger
{
    using System;

    using log4net;

    public static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(string content)
        {
            log.Debug(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }

        public static void Info(string content)
        {
            log.Info(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }

        public static void Warn(string content)
        {
            log.Warn(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }

        public static void Error(string content)
        {
            log.Error(content);

            if (ConfigManager.LogConfiguration.DebugMode)
            {
                Console.WriteLine(content);
            }
        }
    }
}
