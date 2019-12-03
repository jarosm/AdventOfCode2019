using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3b
{
    class Program
    {
        public class PositionAndSteps
        {
            public (int x, int y) Position { get; set; }
            public int StepCount { get; set; }
        }

        public class PositionComparer : EqualityComparer<PositionAndSteps>
        {
            public override bool Equals(PositionAndSteps x, PositionAndSteps y) => x.Position.Equals(y.Position);
            public override int GetHashCode(PositionAndSteps item) => item.Position.GetHashCode();
        }


        static public List<PositionAndSteps> ReadWireData(string data)
        {
            List<PositionAndSteps> wire = new List<PositionAndSteps>();
            string[] steps = data.Split(',');
            int x = 0, y = 0, c = 0;
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
                    wire.Add(new PositionAndSteps() { Position = (x, y), StepCount = ++c });
                }
            }
            return wire;
        }

        static void Main(string[] args)
        {
            List<PositionAndSteps> firstWire = null;
            List<PositionAndSteps> secondWire = null;
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

            PositionAndSteps[] intersectionPoints = firstWire.Intersect(secondWire, new PositionComparer()).ToArray();
            for (int i = 0; i < intersectionPoints.Length; i++)
            {
                int steps1 = firstWire.Where(p => p.Position == intersectionPoints[i].Position).Single().StepCount;
                int steps2 = secondWire.Where(p => p.Position == intersectionPoints[i].Position).Single().StepCount;
                if (steps1 + steps2 < distance)
                    distance = steps1 + steps2;
            }

            Console.WriteLine(distance.ToString());
            Console.ReadLine();
        }
    }
}
