namespace ACrashProgram
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var zero = 0;
            Thread.Sleep(10000);
            var result = 1 / zero;
        }
    }
}