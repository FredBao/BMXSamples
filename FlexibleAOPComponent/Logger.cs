namespace FlexibleAOPComponent
{
    using log4net;
    using log4net.Config;

    public class Logger
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(string content)
        {
            log.Debug(content);
        }

        public static void Info(string content)
        {
            log.Info(content);
        }

        public static void Warn(string content)
        {
            log.Warn(content);
        }

        public static void Error(string content)
        {
            log.Error(content);
        }
    }
}
