namespace SocketAsyncEventArgsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AsynchronousClient client = new AsynchronousClient();

            client.StartClient("127.0.0.1", 11000);
        }
    }
}