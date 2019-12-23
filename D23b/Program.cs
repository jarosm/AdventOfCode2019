using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D23b
{
    public class Worker
    {
        private Int64 address;
        private bool init = true;
        private Int64[] steps;
        private ConcurrentQueue<(Int64 x, Int64 y)>[] queue;
        private int[] readCount;

        public Worker(Int64 _address, Int64[] _steps, ConcurrentQueue<(Int64 x, Int64 y)>[] _queue, int[] _readCount)
        {
            address = _address;
            steps = _steps;
            queue = _queue;
            readCount = _readCount;
        }

        public void IntProgram()
        {
            try
            {
                bool end = false, inputFound = false;
                Int64 relativeBase = 0, index = 0, outAddress = -1, outX = -1, outY = -1;
                (Int64 x, Int64 y) input = (-1, -1);
                int outStep = 0;

                while (!end && (index < steps.Length))
                {
                    Thread.Sleep(10);

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
                            if (init)
                            {
                                steps[first] = address;
                                init = false;
                            }
                            else
                            {
                                if (inputFound)
                                {
                                    steps[first] = input.y;
                                    inputFound = false;
                                }
                                else
                                {
                                    if (queue[address].IsEmpty)
                                    {
                                        steps[first] = -1;
                                        readCount[address] = 1;
                                    }
                                    else
                                    {
                                        if (queue[address].TryDequeue(out input))
                                        {
                                            steps[first] = input.x;
                                            inputFound = true;
                                            readCount[address] = 0;
                                        }
                                    }
                                }
                            }

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
                            switch (outStep)
                            {
                                case 0:
                                    outAddress = steps[first];
                                    readCount[address] = 0;
                                    outStep++;
                                    break;
                                case 1:
                                    outX = steps[first];
                                    outStep++;
                                    break;
                                case 2:
                                    outY = steps[first];
                                    outStep = 0;
                                    Console.WriteLine(outAddress + " -> " + outX + " : " + outY);
                                    queue[outAddress].Enqueue((outX, outY));
                                    break;
                            }

                            index += 2;
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
    }

    public class NAT
    {
        private Int64 address;
        private ConcurrentQueue<(Int64 x, Int64 y)>[] queue;
        private int[] readCount;

        public NAT(Int64 _address, ConcurrentQueue<(Int64 x, Int64 y)>[] _queue, int[] _readCount)
        {
            address = _address;
            queue = _queue;
            readCount = _readCount;
        }

        public void DoWork()
        {
            Int64 oldVal = 0;

            while (true)
            {
                (Int64 x, Int64 y) input = (-1, -1);

                Thread.Sleep(10);
                while (queue[address].Count > 1)
                    queue[address].TryDequeue(out input);

                if (readCount.Sum() == 50 && queue[address].Count == 1)
                {
                    if (queue[address].TryDequeue(out input))
                    {
                        Console.WriteLine(oldVal + " : " + input.y);
                        oldVal = input.y;
                        queue[0].Enqueue(input);
                    }
                }
            }
        }
    }

    class Program
    {
        static string prog = "";
        const int compCount = 50;
        static Thread[] computers = new Thread[compCount];
        static ConcurrentQueue<(Int64 x, Int64 y)>[] queues = new ConcurrentQueue<(Int64 x, Int64 y)>[256];
        static int[] readCount = new int[compCount];

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

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D23\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    prog += line;
                }
            }

            queues[255] = new ConcurrentQueue<(long x, long y)>();
            Thread nat = new Thread(new ThreadStart(new NAT(255, queues, readCount).DoWork)) { IsBackground = true };
            nat.Start();

            for (int i = 0; i < compCount; i++)
            {
                queues[i] = new ConcurrentQueue<(long x, long y)>();
                Worker w = new Worker(i, GetIntArray(prog), queues, readCount);
                computers[i] = new Thread(new ThreadStart(w.IntProgram)) { IsBackground = true };
                computers[i].Start();
            }

            Console.WriteLine("Working...");
            Console.ReadLine();
        }
    }
}