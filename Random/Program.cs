namespace Random
{
    using System;
    using System.Linq;

    using FanucToolManageDll;

    class Program
    {

        static void Main(string[] args)
        {
            //var test=new Test();

            //test.TestMethod();

            //test.TestMethod2("Fred");

            //var visualValue = Convert.ToUInt16(170);

            //var bitArray = Convert.ToString(visualValue, 2).PadLeft(8, '0').Reverse().ToArray();

            //for (var i = 0; i < bitArray.Length; ++i)
            //{
            //    Console.Write(bitArray[i]);  
            //}

            //Console.ReadKey();

            try
            {
                var ftm = new FanucToolManageDll(50);

                uint cutterCompensationMax;

                if (ftm.GetThresholdValue2(out cutterCompensationMax))
                {
                    Console.WriteLine($"输入最大上限{cutterCompensationMax},成功");
                }
                else
                {
                    Console.WriteLine($"输入最大上限{cutterCompensationMax},失败");
                }

                //if (ftm.Connect("10.10.100.39", 8193, 3000))
                //{
                //    Console.WriteLine("Connected!");


                //    if (ftm.GetThresholdValue(5013, out cutterCompensationMax))
                //    {
                //        Console.WriteLine($"输入最大上限{cutterCompensationMax}");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Connect Failed!");
                //}

                ftm.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadKey();
        }
    }
}