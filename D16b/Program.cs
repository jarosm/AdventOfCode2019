using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D16b
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] signal;
            int inputLength = 0, offset = 0;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D16\\input.txt"))
            {
                string line = "", inStr = "";
                while ((line = input.ReadLine()) != null)
                {
                    inStr += line;
                }

                offset = Convert.ToInt32(inStr.Substring(0, 7));
                string temp = "";
                for (int i = 0; i < 10000; i++)
                    temp += inStr;
                temp = temp.Substring(offset);
                inputLength = temp.Length;
                signal = new int[inputLength];
                for (int i = 0; i < inputLength; i++)
                {
                    signal[i] = Convert.ToInt32(temp[i].ToString());
                }
            }

            for (int c = 0; c < 100; c++)
            {
                for (int j = inputLength - 1; j >= 0; j--)
                    signal[j] = Math.Abs((j + 1 < inputLength ? signal[j + 1] : 0) + signal[j]) % 10;
            }

            for (int i = 0; i < 8; i++)
                Console.Write(signal[i]);
            Console.WriteLine();
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}