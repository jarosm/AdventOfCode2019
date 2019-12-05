using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D5b
{
    class Program
    {
        static int[] GetIntArray(string prog)
        {
            string[] steps = prog.Split(',');
            List<int> listOfInts = new List<int>();
            for (int i = 0; i < steps.Length; i++)
            {
                listOfInts.Add(Convert.ToInt32(steps[i]));
            }
            return listOfInts.ToArray();
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D5\\input.txt"))
            {
                string prog = "", line = "";
                while ((line = input.ReadLine()) != null)
                {
                    prog += line;
                }

                try
                {
                    int[] steps = GetIntArray(prog);
                    int index = 0;
                    bool end = false;

                    while (!end && (index < steps.Length))
                    {
                        int first = 0, second = 0, temp = 0;

                        switch (steps[index] % 100)
                        {
                            case 1: // +
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                temp = temp / 10;
                                if (temp % 10 == 1)
                                    second = index + 2;
                                else
                                    second = steps[index + 2];

                                steps[steps[index + 3]] = steps[first] + steps[second];

                                index += 4;
                                break;

                            case 2: // *
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                temp = temp / 10;
                                if (temp % 10 == 1)
                                    second = index + 2;
                                else
                                    second = steps[index + 2];

                                steps[steps[index + 3]] = steps[first] * steps[second];

                                index += 4;
                                break;

                            case 3: // input
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                Console.Write("Input: ");
                                steps[first] = Convert.ToInt32(Console.ReadLine());

                                index += 2;
                                break;

                            case 4: // output
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                Console.WriteLine("Output: " + steps[first].ToString());

                                index += 2;
                                break;

                            case 5: // jnz
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                temp = temp / 10;
                                if (temp % 10 == 1)
                                    second = index + 2;
                                else
                                    second = steps[index + 2];

                                if (steps[first] != 0)
                                    index = steps[second];
                                else
                                    index += 3;
                                break;

                            case 6: // jz
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                temp = temp / 10;
                                if (temp % 10 == 1)
                                    second = index + 2;
                                else
                                    second = steps[index + 2];

                                if (steps[first] == 0)
                                    index = steps[second];
                                else
                                    index += 3;
                                break;

                            case 7: // <
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                temp = temp / 10;
                                if (temp % 10 == 1)
                                    second = index + 2;
                                else
                                    second = steps[index + 2];

                                if (steps[first] < steps[second])
                                    steps[steps[index + 3]] = 1;
                                else
                                    steps[steps[index + 3]] = 0;

                                index += 4;
                                break;

                            case 8: // ==
                                temp = steps[index] / 100;
                                if (temp % 10 == 1)
                                    first = index + 1;
                                else
                                    first = steps[index + 1];
                                temp = temp / 10;
                                if (temp % 10 == 1)
                                    second = index + 2;
                                else
                                    second = steps[index + 2];

                                if (steps[first] == steps[second])
                                    steps[steps[index + 3]] = 1;
                                else
                                    steps[steps[index + 3]] = 0;

                                index += 4;
                                break;

                            case 99:
                                end = true;
                                index++;
                                break;

                            default:
                                end = true;
                                break;
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
