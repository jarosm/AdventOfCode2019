using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D22a
{
    class Program
    {
        static int cardCount = 10007;
        static int[] stack;

        static int[] NewStack(int[] s)
        {
            return s.Reverse().ToArray();
        }

        static int[] Increment(int[] s, int inc)
        {
            int[] ts = new int[cardCount];
            int index = 0;
            for (int i = 0; i < cardCount; i++)
            {
                ts[index] = s[i];
                index = (index + inc) % cardCount;
            }
            return ts;
        }

        static int[] Cut(int[] s, int cut)
        {
            int[] ts = new int[cardCount];
            if (cut >= 0)
            {
                Array.Copy(s, 0, ts, cardCount - cut, cut);
                Array.Copy(s, cut, ts, 0, cardCount - cut);
            }
            else
            {

                Array.Copy(s, cardCount + cut, ts, 0, Math.Abs(cut));
                Array.Copy(s, 0, ts, Math.Abs(cut), cardCount + cut);
            }
            return ts;
        }

        static void Main(string[] args)
        {
            stack = new int[cardCount];
            for (int i = 0; i < cardCount; i++)
                stack[i] = i;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D22\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    if (line == "deal into new stack")
                        stack = NewStack(stack);
                    else if (line.StartsWith("deal with increment"))
                        stack = Increment(stack, Convert.ToInt32(line.Replace("deal with increment ", "")));
                    else if (line.StartsWith("cut"))
                        stack = Cut(stack, Convert.ToInt32(line.Replace("cut ", "")));
                }
            }

            Console.WriteLine(stack.ToList().FindIndex(a => a == 2019));
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
