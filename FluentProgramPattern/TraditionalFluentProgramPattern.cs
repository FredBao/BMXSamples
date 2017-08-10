namespace FluentProgramPattern
{
    using System;

    public class RequestConfigContext
    {
        public static RequestConfigContext configContext = new RequestConfigContext();

        public string Content { get; set; }

        public string Format { get; set; }

        public RequestProtocol Protocol { get; set; }

        public Func<RequestContext, bool> SafetyCheck { get; set; }

        public int Size { get; set; }
    }

    public class RequestConfigManager
    {
        public static RequestConfigManager configManager = new RequestConfigManager();

        public RequestConfigManager SetGlobalRequestFormatJson()
        {
            if (string.IsNullOrEmpty(RequestConfigContext.configContext.Format))
            {
                RequestConfigContext.configContext.Format = "Json";
            }

            return this;
        }

        public RequestConfigManager SetGlobalRequestFormatXml()
        {
            if (string.IsNullOrEmpty(RequestConfigContext.configContext.Format))
            {
                RequestConfigContext.configContext.Format = "Xml";
            }

            return this;
        }

        public RequestConfigManager SetGlobalRequestProtocol(RequestProtocol protocol)
        {
            RequestConfigContext.configContext.Protocol = protocol;
            return this;
        }

        public RequestConfigManager SetGlobalRequestSize(int size)
        {
            RequestConfigContext.configContext.Size = size;
            return this;
        }
    }
}