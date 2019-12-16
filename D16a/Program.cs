using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D16a
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] signal;
            int[][] pattern;
            int inputLength = 0;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D16\\input.txt"))
            {
                string line = "", inStr = "";
                while ((line = input.ReadLine()) != null)
                {
                    inStr += line;
                }

                inputLength = inStr.Length;
                signal = new int[inputLength];
                for (int i = 0; i < inputLength; i++)
                {
                    signal[i] = Convert.ToInt32(inStr[i].ToString());
                }
            }

            pattern = new int[inputLength][];
            for (int i = 0; i < inputLength; i++)
            {
                pattern[i] = new int[inputLength];
                int counter = 0;
                bool skip = true;
                while (counter < inputLength)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (skip)
                        {
                            skip = false;
                            continue;
                        }
                        pattern[i][counter] = 0;
                        counter++;
                        if (counter >= inputLength)
                            goto nextStep;
                    }
                    for (int j = 0; j <= i; j++)
                    {
                        pattern[i][counter] = 1;
                        counter++;
                        if (counter >= inputLength)
                            goto nextStep;
                    }
                    for (int j = 0; j <= i; j++)
                    {
                        pattern[i][counter] = 0;
                        counter++;
                        if (counter >= inputLength)
                            goto nextStep;
                    }
                    for (int j = 0; j <= i; j++)
                    {
                        pattern[i][counter] = -1;
                        counter++;
                        if (counter >= inputLength)
                            goto nextStep;
                    }
                }
nextStep:
                ;
            }

            for (int c = 0; c < 100; c++)
            {
                int[] tempSignal = new int[inputLength];
                for (int i = 0; i < inputLength; i++)
                {
                    int dotProduct = 0;
                    for (int j = 0; j < inputLength; j++)
                    {
                        dotProduct += signal[j] * pattern[i][j];
                    }
                    tempSignal[i] = Math.Abs(dotProduct % 10);
                }
                signal = tempSignal;
            }

            Console.WriteLine(string.Join("", signal).Substring(0, 8));
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
