namespace EFCodeFirstToAnExistingDatabase
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var p = new PartsOnlineRecordRepository();

            Console.WriteLine(p.Count());

            Console.ReadKey();
        }
    }
}
