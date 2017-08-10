namespace FluentProgramPattern
{
    public class RequestProtocol
    {
        public const string InternalSoa = "Soa";

        public const string Soap = "SOap";

        public RequestProtocol(string protocol)
        {
            this._protocol = protocol;
        }

        public string _protocol { get; set; }
    }
}