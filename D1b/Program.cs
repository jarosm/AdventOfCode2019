using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1b
{
    class Program
    {
        static void Main(string[] args)
        {
            int fuelSum = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D1a\\input.txt"))
            {
                string number = "";
                while ((number = input.ReadLine()) != null)
                {
                    try
                    {
                        int num = Convert.ToInt32(number);
                        while (num > 0)
                        {
                            num = num / 3 - 2;
                            if (num > 0)
                                fuelSum += num;
                        }
                    }
                    catch { }
                }
            }
            Console.WriteLine(fuelSum);
            Console.ReadLine();
        }
    }
}
