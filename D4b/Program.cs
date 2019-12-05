using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4b
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
                int[] digits = GetIntArray(i);
                bool valid = true, twoSame = false;
                int prevDigit = 0, doubleCount = 0, doubleDigit = 0;

                for (int k = 0; k < digits.Length; k++)
                {
                    if (prevDigit < digits[k])
                        doubleCount = 0;
                    if (prevDigit == digits[k])
                    {
                        doubleCount++;
                        if ((doubleCount > 1) && (doubleDigit == digits[k]))
                        {
                            doubleDigit = 0;
                            twoSame = false;
                        }
                        else
                        {
                            if ((doubleCount == 1) && (doubleDigit == 0))
                            {
                                doubleDigit = digits[k];
                                twoSame = true;
                            }
                        }
                    }
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
