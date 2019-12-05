using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4a
{
    class Program
    {
        static int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        static void Main(string[] args)
        {
            int count = 0, min = 136818, max = 685979;

            for (int i = min; i <= max; i++)
            {
                Console.WriteLine(i.ToString());
                int[] digits = GetIntArray(i);
                bool valid = true, twoSame = false;
                int prevDigit = 0;

                for (int k = 0; k < digits.Length; k++)
                {
                    if (prevDigit == digits[k])
                        twoSame = true;
                    if (prevDigit > digits[k])
                    {
                        valid = false;
                        break;
                    }
                    prevDigit = digits[k];
                }

                if (valid && twoSame)
                    count++;
            }

            Console.WriteLine("count: " + count.ToString());
            Console.ReadLine();
        }
    }
}
