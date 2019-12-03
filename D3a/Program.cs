using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3a
{
    class Program
    {
        static public List<(int x, int y)> ReadWireData(string data)
        {
            List<(int x, int y)> wire = new List<(int x, int y)>();
            string[] steps = data.Split(',');
            int x = 0, y = 0;
            for (int i = 0; i < steps.Length; i++)
            {
                int dist = Convert.ToInt32(steps[i].Substring(1));
                char dir = steps[i][0];
                for (int d = 0; d < dist; d++)
                {
                    switch (dir)
                    {
                        case 'U':
                            y++;
                            break;
                        case 'D':
                            y--;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'R':
                            x++;
                            break;
                    }
                    wire.Add((x, y));
                }
            }
            return wire;
        }

        static void Main(string[] args)
        {
            List<(int x, int y)> firstWire = null;
            List<(int x, int y)> secondWire = null;
            int distance = Int32.MaxValue;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D3\\input.txt"))
            {
                string line = "";
                if ((line = input.ReadLine()) != null)
                {
                    firstWire = ReadWireData(line);
                }
                line = "";
                if ((line = input.ReadLine()) != null)
                {
                    secondWire = ReadWireData(line);
                }                
            }

            (int x, int y)[] intersectionPoints = firstWire.Intersect(secondWire).ToArray();
            distance = intersectionPoints.Min(p => Math.Abs(p.x) + Math.Abs(p.y));

            Console.WriteLine(distance.ToString());
            Console.ReadLine();
        }
    }
}
