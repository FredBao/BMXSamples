namespace PipelinePatternDemo
{
    public class Request
    {
        public string Content { get; set; }

        public string RequestHead { get; set; }

        public RequestType Type { get; set; }
    }

    public enum RequestType
    {
        App = 0,

        DotNET = 1
    }
}