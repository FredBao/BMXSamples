namespace BMX
{
    public static class ConfigManager
    {
        public static class EntityFrameworkConfiguration
        {
            public static string ConnectionString { get; set; }
        }

        public static class MongoDbConfiguration
        {
            public static string ConnectionString { get; set; }

            public static string DatabaseName { get; set; }
        }

        public static class LogConfiguration
        {
            public static bool DebugMode { get; set; }
        }
    }
}