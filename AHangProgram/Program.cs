namespace AHangProgram
{
    using System;
    using System.Threading;

    class Program
    {
        private static object syncObj = new object();

        protected static void SharedMesthod()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is waiting!");
            lock (syncObj)
            {
                while (true)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has the lock!");
                    Thread.Sleep(1000);
                }
            }
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(SharedMesthod);
                t.Start();
            }

            Console.WriteLine($"主线程挂起!");
            Console.ReadKey();
        }
    }
}