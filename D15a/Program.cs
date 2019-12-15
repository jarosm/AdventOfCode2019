using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D15a
{
    class Program
    {
        static string prog = "";
        static Int64[] steps = null;
        static Int64 relativeBase = 0;
        static int maxX = 41, maxY = 41, startPosX = 21, startPosY = 21, endPosX = 0, endPosY = 0, x = startPosX, y = startPosY;
        static char[,] field = new char[maxX, maxY];

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

        static void CreateMaze()
        {
            steps = GetIntArray(prog);

            Int64 type = 0, index = 0, direction = 1;
            int status = 0;
            bool end = false;

            while (!end)
            {
                IntProgram(direction, ref type, ref index, ref status);

                if (status == 1)
                    break;

                switch (type)
                {
                    case 0:
                        switch (direction)
                        {
                            case 1: // north
                                direction = 3;
                                field[x, y - 1] = '#';
                                break;
                            case 2: // south
                                direction = 4;
                                field[x, y + 1] = '#';
                                break;
                            case 3: // west
                                direction = 2;
                                field[x - 1, y] = '#';
                                break;
                            case 4: // east
                                direction = 1;
                                field[x + 1, y] = '#';
                                break;
                        }
                        break;
                    case 1:
                        switch (direction)
                        {
                            case 1: // north
                                direction = 4;
                                y--;
                                break;
                            case 2: // south
                                direction = 3;
                                y++;
                                break;
                            case 3: // west
                                direction = 1;
                                x--;
                                break;
                            case 4: // east
                                direction = 2;
                                x++;
                                break;
                        }
                        field[x, y] = '.';
                        break;
                    case 2:
                        switch (direction)
                        {
                            case 1: // north
                                y--;
                                break;
                            case 2: // south
                                y++;
                                break;
                            case 3: // west
                                x--;
                                break;
                            case 4: // east
                                x++;
                                break;
                        }
                        field[x, y] = 'O';
                        end = true;
                        break;
                }
            }

            steps = GetIntArray(prog);
            index = 0; direction = 1; relativeBase = 0;
            x = startPosX; y = startPosY;
            end = false;

            while (!end)
            {
                IntProgram(direction, ref type, ref index, ref status);

                if (status == 1)
                    break;

                switch (type)
                {
                    case 0:
                        switch (direction)
                        {
                            case 1: // north
                                direction = 4;
                                field[x, y - 1] = '#';
                                break;
                            case 2: // south
                                direction = 3;
                                field[x, y + 1] = '#';
                                break;
                            case 3: // west
                                direction = 1;
                                field[x - 1, y] = '#';
                                break;
                            case 4: // east
                                direction = 2;
                                field[x + 1, y] = '#';
                                break;
                        }
                        break;
                    case 1:
                        switch (direction)
                        {
                            case 1: // north
                                direction = 3;
                                y--;
                                break;
                            case 2: // south
                                direction = 4;
                                y++;
                                break;
                            case 3: // west
                                direction = 2;
                                x--;
                                break;
                            case 4: // east
                                direction = 1;
                                x++;
                                break;
                        }
                        field[x, y] = '.';
                        break;
                    case 2:
                        switch (direction)
                        {
                            case 1: // north
                                y--;
                                break;
                            case 2: // south
                                y++;
                                break;
                            case 3: // west
                                x--;
                                break;
                            case 4: // east
                                x++;
                                break;
                        }
                        field[x, y] = 'O';
                        endPosX = x;
                        endPosY = y;
                        end = true;
                        break;
                }
            }

            field[startPosX, startPosY] = 'D';
        }

        static int GetDistance()
        {
            List<(int x, int y, int d)> queue = new List<(int x, int y, int d)>();
            for (int j = 0; j < maxY; j++)
            {
                for (int i = 0; i < maxX; i++)
                {
                    if (field[i, j] == '.')
                        queue.Add((i, j, int.MaxValue));
                }
            }
            queue.Add((startPosX, startPosY, 0));
            queue.Add((endPosX, endPosY, int.MaxValue));

            while (queue.Count > 0)
            {
                int current = queue.FindIndex(a => a.d == queue.Min(b => b.d));
                x = queue[current].x;
                y = queue[current].y;
                int dist = queue[current].d;
                queue.RemoveAt(current);

                if (x == endPosX && y == endPosY)
                    return dist;

                for (int i = 0; i < 4; i++)
                {
                    int x2 = -1, y2 = -1;
                    switch (i)
                    {
                        case 0:
                            x2 = x;
                            y2 = y - 1;
                            break;
                        case 1:
                            x2 = x + 1;
                            y2 = y;
                            break;
                        case 2:
                            x2 = x;
                            y2 = y + 1;
                            break;
                        case 3:
                            x2 = x - 1;
                            y2 = y;
                            break;
                    }

                    int index2 = queue.FindIndex(a => a.x == x2 && a.y == y2);
                    if (index2 >= 0)
                    {
                        if (dist + 1 < queue[index2].d)
                            queue[index2] = (x2, y2, dist + 1);
                    }
                }
            }

            return 0;
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D15\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    prog += line;
                }
            }

            CreateMaze();

            for (int j = 0; j < maxY; j++)
            {
                for (int i = 0; i < maxX; i++)
                {
                    Console.Write(field[i, j] == 0 ? '?' : field[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine(GetDistance());
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
