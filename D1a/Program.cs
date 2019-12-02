using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1a
{
    class Program
    {
        static void Main(string[] args)
        {
            int fuelSum = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D1\\input.txt"))
            {
                string number = "";
                while ((number = input.ReadLine()) != null)
                {
                    try
                    {
                        int num = Convert.ToInt32(number);
                        num = num / 3 - 2;
                        fuelSum += num;
                    }
                    catch { }
                }
            }
            Console.WriteLine(fuelSum);
            Console.ReadLine();
        }
    }
}
