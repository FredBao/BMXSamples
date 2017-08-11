namespace BMX
{
    public static class ConfigManager
    {
        public static string MongoDatabaseName { get; private set; }

        public static string MongoConnectionString { get; private set; }

        public static string SqlServerConnection { get; private set; }
    }
}