namespace Hangfire
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static object SyncLock = new object();

        public static void DoWork(int i)
        {
            Thread.Sleep(1000);
            Console.WriteLine(i + " is waiting.");
            lock (SyncLock)
            {
                try
                {
                    if (i == 1)
                    {
                        throw new NotImplementedException();
                    }

                    Console.WriteLine(i + " is in.");

                    var t1 = Task.Run(
                        () =>
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine(i + "t1");
                            });
                    var t2 = Task.Run(
                        () =>
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine(i + "t2");
                            });
                    var t3 = Task.Run(
                        () =>
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine(i + "t3");
                            });

                    Task.WaitAll(t1, t2, t3);
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(i + " occurs an error.");
                }
            }

            Thread.Sleep(1000);
            Console.WriteLine(i + " is done.");
        }

        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseColouredConsoleLogProvider().UseSqlServerStorage(
                @"Server=(localdb)\MSSQLLocalDB;Database=Hangfire;Trusted_Connection=true;");

            // 支持基于队列的任务处理：任务执行不是同步的，而是放到一个持久化队列中，以便马上把请求控制权返回给调用者。
            // BackgroundJob.Enqueue(() => Console.WriteLine("Simple!"));
            // 延迟任务执行：不是马上调用方法，而是设定一个未来时间点再来执行。            
            // BackgroundJob.Schedule(() => Console.WriteLine("Reliable!"), TimeSpan.FromSeconds(5));
            // 循环任务执行：一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。
            // RecurringJob.AddOrUpdate(() => Console.WriteLine("Transparent!"), Cron.Minutely);//注意最小单位是分钟
            using (new BackgroundJobServer())
            {
                Console.WriteLine(BackgroundJob.Enqueue(() => DoWork(1)));
                Console.WriteLine(BackgroundJob.Enqueue(() => DoWork(2)));
                Console.WriteLine(BackgroundJob.Enqueue(() => DoWork(3)));

                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}