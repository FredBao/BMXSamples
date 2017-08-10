namespace FluentProgramPattern
{
    public class RequestContext
    {
        public string Content { get; set; }

        public string Format { get; set; }

        public RequestProtocol Protocol { get; set; }

        public int Size { get; set; }
    }
}