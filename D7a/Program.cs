using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D7a
{
    class Program
    {
        static string prog = "";

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

        static int IntProgram(int firstInput, int secondInput)
        {
            int output = 0, inputCounter = 0;

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
                            
                            switch (inputCounter)
                            {
                                case 0:
                                    steps[first] = firstInput;
                                    break;
                                case 1:
                                    steps[first] = secondInput;
                                    break;
                                default:
                                    break;
                            }
                            inputCounter++;                            

                            index += 2;
                            break;

                        case 4: // output
                            temp = steps[index] / 100;
                            if (temp % 10 == 1)
                                first = index + 1;
                            else
                                first = steps[index + 1];
                            output = steps[first];

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

            return output;
        }

        static void Main(string[] args)
        {
            int max = 0;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D7\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    prog += line;
                }

                for (int a = 0; a <= 4; a++)
                {
                    for (int b = 0; b <= 4; b++)
                    {
                        for (int c = 0; c <= 4; c++)
                        {
                            for (int d = 0; d <= 4; d++)
                            {
                                for (int e = 0; e <= 4; e++)
                                {
                                    if ((a == b) || (a == c) || (a == d) || (a == e) || (b == c) || (b == d) || (b == e) || (c == d) || (c == e) || (d == e))
                                        continue;
                                    int temp = IntProgram(e, IntProgram(d, IntProgram(c, IntProgram(b, IntProgram(a, 0)))));
                                    Console.WriteLine(a.ToString() + b.ToString() + c.ToString() + d.ToString() + e.ToString() + " : " + temp.ToString());
                                    if (temp > max)
                                        max = temp;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("max output = " + max.ToString());
            Console.ReadLine();
        }
    }
}
