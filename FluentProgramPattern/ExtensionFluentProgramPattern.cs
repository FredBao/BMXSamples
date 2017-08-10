namespace FluentProgramPattern
{
    using System;

    public static class ExtensionFluentProgramPattern
    {
        public static void SetGlobalRequestFormatBinary(this RequestConfigManager configManager)
        {
            if (string.IsNullOrEmpty(RequestConfigContext.configContext.Format))
            {
                RequestConfigContext.configContext.Format = "Binary";
            }
        }

        public static void SetGlobalRequestSatetyCheck(
            this RequestConfigManager configManager,
            Func<RequestContext, bool> safetyCheck)
        {
            RequestConfigContext.configContext.SafetyCheck = safetyCheck;
        }
    }
}