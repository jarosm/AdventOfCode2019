using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace D22b
{
    class Program
    {
        static Int64 cardCount = 119315717514047;
        static Int64 iter = 101741582076661;
        static Int64 position = 2020;

        static Int64 ModInverse(Int64 input, Int64 power, Int64 modulo)
        {
            Int64 original = modulo;

            Int64 y = 0;
            Int64 x = 1;

            if (original == 1)
                return 0;

            while (input > 1)
            {
                Int64 q = input / modulo;
                Int64 t = modulo;
                modulo = input % modulo;
                input = t;
                t = y;
                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += original;

            return x;
        }

        static Int64 ModuloMultiplication(Int64 a, Int64 b, Int64 mod)
        {
            Int64 res = 0; // Initialize result 

            // Update a if it is more than 
            // or equal to mod 
            a %= mod;

            while (b > 0)
            {
                // If b is odd, add a with result 
                if (b % 2 == 1)
                    res = (res + a) % mod;

                // Here we assume that doing 2*a 
                // doesn't cause overflow 
                a = (2 * a) % mod;

                b >>= 1; // b = b / 2 
            }

            return res;
        }

        static Int64 modular(Int64 b, Int64 exp, Int64 mod)
        {
            Int64 x = 1;
            int i;
            Int64 power = b % mod;

            for (i = 0; i < sizeof(Int64) * 8; i++)
            {
                Int64 least_sig_bit = 0x00000001 & (exp >> i);
                if (least_sig_bit == 1)
                    x = ModuloMultiplication(x, power, mod);
                power = ModuloMultiplication(power, power, mod);
            }

            return x;
        }

        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D22\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            Int64 a = 1, b = 0;
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i] == "deal into new stack")
                {
                    b++;
                    b *= -1;
                    a *= -1;
                }
                else if (lines[i].StartsWith("deal with increment"))
                {
                    Int64 inc = Convert.ToInt64(lines[i].Replace("deal with increment ", ""));
                    Int64 inversePower = ModInverse(inc, iter, cardCount);
                    a = ModuloMultiplication(a, inversePower, cardCount);
                    b = ModuloMultiplication(b, inversePower, cardCount);
                }
                else if (lines[i].StartsWith("cut"))
                {
                    Int64 cut = Convert.ToInt64(lines[i].Replace("cut ", ""));
                    if (cut < 0)
                        cut += cardCount;
                    b += cut;
                }

                a %= cardCount;
                b %= cardCount;
                if (b < 0)
                    b += cardCount;
                if (a < 0)
                    a += cardCount;
            }

            Int64 first = ModuloMultiplication(modular(a, iter, cardCount), position, cardCount);
            Int64 second = (modular(a, iter, cardCount) + cardCount - 1) % cardCount;
            Int64 third = ModuloMultiplication(b, second, cardCount);
            Int64 fourth = modular(a - 1, cardCount - 2, cardCount);

            Console.WriteLine((first + ModuloMultiplication(third, fourth, cardCount)) % cardCount);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
