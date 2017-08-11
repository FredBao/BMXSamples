namespace Random
{
    using System;
    using System.Linq;

    class Program
    {

        static void Main(string[] args)
        {
            //var test=new Test();

            //test.TestMethod();

            //test.TestMethod2("Fred");

            var visualValue = Convert.ToUInt16(170);

            var bitArray = Convert.ToString(visualValue, 2).PadLeft(8, '0').Reverse().ToArray();

            for (var i = 0; i < bitArray.Length; ++i)
            {
                Console.Write(bitArray[i]);  
            }

            Console.ReadKey();
        }
    }
}