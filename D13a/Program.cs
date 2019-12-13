using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D13a
{
    class Program
    {
        static string prog = "";
        static Int64[] steps = null;
        static Int64 relativeBase = 0;

        static Int64[] GetIntArray(string prog)
        {
            string[] steps = prog.Split(',');
            List<Int64> listOfInts = new List<Int64>();
            for (int i = 0; i < steps.Length; i++)
            {
                listOfInts.Add(Convert.ToInt64(steps[i]));
            }
            // memory buffer
            for (int i = 0; i < 1000; i++)
            {
                listOfInts.Add(0);
            }
            return listOfInts.ToArray();
        }

        static void IntProgram(Int64 inVal, ref Int64 outVal, ref Int64 index, ref int statusCode)
        {
            try
            {
                bool end = false;

                while (!end && (index < steps.Length))
                {
                    Int64 first = 0, second = 0, third = 0, temp = 0;

                    switch (steps[index] % 100)
                    {
                        case 1: // +
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    second = index + 2;
                                    break;
                                case 2:
                                    second = relativeBase + steps[index + 2];
                                    break;
                                default:
                                    second = steps[index + 2];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    third = index + 3;
                                    break;
                                case 2:
                                    third = relativeBase + steps[index + 3];
                                    break;
                                default:
                                    third = steps[index + 3];
                                    break;
                            }

                            steps[third] = steps[first] + steps[second];

                            index += 4;
                            break;

                        case 2: // *
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    second = index + 2;
                                    break;
                                case 2:
                                    second = relativeBase + steps[index + 2];
                                    break;
                                default:
                                    second = steps[index + 2];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    third = index + 3;
                                    break;
                                case 2:
                                    third = relativeBase + steps[index + 3];
                                    break;
                                default:
                                    third = steps[index + 3];
                                    break;
                            }

                            steps[third] = steps[first] * steps[second];

                            index += 4;
                            break;

                        case 3: // input
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            steps[first] = inVal;

                            index += 2;
                            break;

                        case 4: // output
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            outVal = steps[first];

                            index += 2;

                            end = true;
                            statusCode = 0;
                            break;

                        case 5: // jnz
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    second = index + 2;
                                    break;
                                case 2:
                                    second = relativeBase + steps[index + 2];
                                    break;
                                default:
                                    second = steps[index + 2];
                                    break;
                            }

                            if (steps[first] != 0)
                                index = steps[second];
                            else
                                index += 3;
                            break;

                        case 6: // jz
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    second = index + 2;
                                    break;
                                case 2:
                                    second = relativeBase + steps[index + 2];
                                    break;
                                default:
                                    second = steps[index + 2];
                                    break;
                            }

                            if (steps[first] == 0)
                                index = steps[second];
                            else
                                index += 3;
                            break;

                        case 7: // <
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    second = index + 2;
                                    break;
                                case 2:
                                    second = relativeBase + steps[index + 2];
                                    break;
                                default:
                                    second = steps[index + 2];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    third = index + 3;
                                    break;
                                case 2:
                                    third = relativeBase + steps[index + 3];
                                    break;
                                default:
                                    third = steps[index + 3];
                                    break;
                            }

                            if (steps[first] < steps[second])
                                steps[third] = 1;
                            else
                                steps[third] = 0;

                            index += 4;
                            break;

                        case 8: // ==
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    second = index + 2;
                                    break;
                                case 2:
                                    second = relativeBase + steps[index + 2];
                                    break;
                                default:
                                    second = steps[index + 2];
                                    break;
                            }
                            temp = temp / 10;
                            switch (temp % 10)
                            {
                                case 1:
                                    third = index + 3;
                                    break;
                                case 2:
                                    third = relativeBase + steps[index + 3];
                                    break;
                                default:
                                    third = steps[index + 3];
                                    break;
                            }

                            if (steps[first] == steps[second])
                                steps[third] = 1;
                            else
                                steps[third] = 0;

                            index += 4;
                            break;

                        case 9: // adjust relative index
                            temp = steps[index] / 100;
                            switch (temp % 10)
                            {
                                case 1:
                                    first = index + 1;
                                    break;
                                case 2:
                                    first = relativeBase + steps[index + 1];
                                    break;
                                default:
                                    first = steps[index + 1];
                                    break;
                            }

                            relativeBase += steps[first];

                            index += 2;
                            break;

                        case 99:
                            end = true;
                            statusCode = 1;
                            index++;
                            break;

                        default:
                            end = true;
                            statusCode = 1;
                            break;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D13\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    prog += line;
                }
            }

            steps = GetIntArray(prog);

            Int64 x = 0, y = 0, type = 0, index = 0;
            int status = 0;
            int maxX = 40, maxY = 25;
            Int64[,] field = new Int64[maxX, maxY];

            while (status != 1)
            {
                IntProgram(0, ref x, ref index, ref status);
                if (status == 1)
                    break;
                IntProgram(0, ref y, ref index, ref status);
                if (status == 1)
                    break;
                IntProgram(0, ref type, ref index, ref status);
                if (status == 1)
                    break;

                field[x, y] = type;
            }

            int total = 0;
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    total += (field[i, j] == 2 ? 1 : 0);

            Console.WriteLine(total);

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}