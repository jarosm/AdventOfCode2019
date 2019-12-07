using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D7b
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

        static int IntProgram(int firstInput, int secondInput, int[] steps, ref int index, ref int endState)
        {
            int output = 0;

            try
            {
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

                            if (index > 0)
                                steps[first] = secondInput;
                            else
                                steps[first] = firstInput;

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

                            end = true;
                            endState = 0;
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
                            endState = 1;
                            index++;
                            break;

                        default:
                            end = true;
                            endState = 1;
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

                for (int a = 5; a <= 9; a++)
                {
                    for (int b = 5; b <= 9; b++)
                    {
                        for (int c = 5; c <= 9; c++)
                        {
                            for (int d = 5; d <= 9; d++)
                            {
                                for (int e = 5; e <= 9; e++)
                                {
                                    if ((a == b) || (a == c) || (a == d) || (a == e) || (b == c) || (b == d) || (b == e) || (c == d) || (c == e) || (d == e))
                                        continue;

                                    int[] stepsA = GetIntArray(prog);
                                    int[] stepsB = GetIntArray(prog);
                                    int[] stepsC = GetIntArray(prog);
                                    int[] stepsD = GetIntArray(prog);
                                    int[] stepsE = GetIntArray(prog);

                                    int power = 0, endState = 0, indexA = 0, indexB = 0, indexC = 0, indexD = 0, indexE = 0;

                                    while (endState == 0)
                                    {
                                        power = IntProgram(a, power, stepsA, ref indexA, ref endState);

                                        power = IntProgram(b, power, stepsB, ref indexB, ref endState);

                                        power = IntProgram(c, power, stepsC, ref indexC, ref endState);

                                        power = IntProgram(d, power, stepsD, ref indexD, ref endState);

                                        power = IntProgram(e, power, stepsE, ref indexE, ref endState);

                                        if (power > max)
                                            max = power;
                                    }
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
