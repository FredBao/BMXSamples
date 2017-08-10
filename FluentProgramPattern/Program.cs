namespace FluentProgramPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            

            RequestConfigManager.configManager.SetGlobalRequestSize(10)
                .SetGlobalRequestProtocol(new RequestProtocol(RequestProtocol.InternalSoa)).SetGlobalRequestFormatJson()
                .SetGlobalRequestFormatXml();

            

            #region ExtensionFluentProgramPattern

            RequestConfigManager.configManager.SetGlobalRequestSize(10)
                .SetGlobalRequestProtocol(new RequestProtocol(RequestProtocol.InternalSoa)).SetGlobalRequestFormatJson()
                .SetGlobalRequestFormatXml().SetGlobalRequestFormatBinary();

            RequestConfigManager.configManager.SetGlobalRequestSatetyCheck(context => context.Format == string.Empty);

            #endregion
        }
    }
}