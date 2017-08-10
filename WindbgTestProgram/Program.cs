namespace WindbgTestProgram
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class Program
    {
        private List<byte[]> list = new List<byte[]>();

        static void Main(string[] args)
        {
            var o = new Program();
            o.Test1();
            o.Test2();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        void Test1()
        {
            for (int i = 0; i < 10; i++)
            {
                this.list.Add(new byte[1024 * 1024 * 10]);
            }
        }

        void Test2()
        {
            new Thread(
                () =>
                    {
                        while (true)
                        {
                        }
                    }).Start();
        }
    }
}