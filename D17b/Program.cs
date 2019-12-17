using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D17b
{
    class Program
    {
        static string prog = "";
        static Int64[] steps = null;
        static Int64 relativeBase = 0;
        static List<string> field = new List<string>();

        static Int64[] GetIntArray(string prog)
        {
            string[] steps = prog.Split(',');
            List<Int64> listOfInts = new List<Int64>();
            for (int i = 0; i < steps.Length; i++)
            {
                listOfInts.Add(Convert.ToInt64(steps[i]));
            }
            // memory buffer
            for (int i = 0; i < 6000; i++)
            {
                listOfInts.Add(0);
            }
            return listOfInts.ToArray();
        }

        static void IntProgram(string inVal, ref Int64 outVal, ref Int64 index)
        {
            int statusCode;
            int inValIndex = 0;

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
                            steps[first] = inVal[inValIndex];
                            inValIndex++;

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
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D17\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    prog += line;
                }
            }

            steps = GetIntArray(prog);

            steps[0] = 2;
            Int64 outVal = 0, index = 0;

            string temp = "";
            int inStep = 0;
            while (true)
            {
                switch (inStep)
                {
                    case 1:
                        IntProgram("A,B,A,B,C,C,B,A,B,C\n", ref outVal, ref index);
                        break;
                    case 2:
                        IntProgram("L,10,R,10,L,10,L,10\n", ref outVal, ref index);
                        break;
                    case 3:
                        IntProgram("R,10,R,12,L,12\n", ref outVal, ref index);
                        break;
                    case 4:
                        IntProgram("R,12,L,12,R,6\n", ref outVal, ref index);
                        break;
                    case 5:
                        IntProgram("n\n", ref outVal, ref index);
                        break;
                    default:
                        IntProgram("", ref outVal, ref index);
                        break;
                }

                if (outVal > 255)
                    break;

                temp += (char)outVal;
                if (outVal == 10)
                {
                    Console.Write(temp);
                    if (temp.Contains("Main:"))
                        inStep = 1;
                    if (temp.Contains("Function A:"))
                        inStep = 2;
                    if (temp.Contains("Function B:"))
                        inStep = 3;
                    if (temp.Contains("Function C:"))
                        inStep = 4;
                    if (temp.Contains("video feed"))
                        inStep = 5;

                    if (temp == "\n")
                        Console.ReadLine();

                    temp = "";
                }
            }

            Console.WriteLine(outVal);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}