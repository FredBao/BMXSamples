using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordYield
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Power(2, 8, string.Empty);

            Console.WriteLine(result.Count());

            foreach (int i in result)
            {
                Console.Write("{0} ", i);
            }
            Console.ReadKey();
        }

        public static IEnumerable<int> Power(int number, int exponent, string s)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
            yield return 3;
            yield return 4;
            yield return 5;
        }
    }
}
