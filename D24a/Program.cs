using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D24a
{
    class Program
    {
        static List<int[,]> fields = new List<int[,]>();

        static void AdvanceGeneration(int index)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    int sum = (x > 0 ? fields[index - 1][y, x - 1] : 0)
                        + (y > 0 ? fields[index - 1][y - 1, x] : 0)
                        + (x < 4 ? fields[index - 1][y, x + 1] : 0)
                        + (y < 4 ? fields[index - 1][y + 1, x] : 0);
                    if (fields[index - 1][y, x] == 1)
                    {
                        if (sum != 1)
                            fields[index][y, x] = 0;
                        else
                            fields[index][y, x] = 1;
                    }
                    else
                    {
                        if (sum == 1 || sum == 2)
                            fields[index][y, x] = 1;
                        else
                            fields[index][y, x] = 0;
                    }
                }
            }
        }

        static bool FindEqual(int index)
        {
            for (int i = 0; i < index; i++)
            {
                bool found = true;
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (fields[i][y, x] != fields[index][y, x])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (!found)
                        break;
                }
                if (found)
                    return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D24\\input.txt"))
            {
                string line = "";
                fields.Add(new int[5, 5]);
                int c = 0;
                while ((line = input.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                        fields[0][c, i] = line[i] == '.' ? 0 : 1;
                    c++;
                }
            }

            bool found = false;
            int index = 0;
            while (!found)
            {
                fields.Add(new int[5, 5]);
                index++;
                AdvanceGeneration(index);
                found = FindEqual(index);
            }

            double rating = 0;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (fields[index][y, x] == 1)
                        rating += Math.Pow(2, 5 * y + x);
                }
            }

            Console.WriteLine(rating);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}