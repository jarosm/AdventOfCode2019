using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D20a
{
    class Program
    {
        const int start = 'A' * 100 + 'A';
        const int end = 'Z' * 100 + 'Z';
        static P startPosition;
        static P[][] map;

        private struct P : IEquatable<P>
        {
            public bool Passable { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Portal { get; set; }
            public int OppositePortalX { get; set; }
            public int OppositePortalY { get; set; }


            public bool Equals(P other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((P)obj);
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 17;
                    // Suitable nullity checks etc, of course :)
                    hash = hash * 23 + X.GetHashCode();
                    hash = hash * 23 + Y.GetHashCode();
                    return hash;
                }
            }
        }

        static int GetDistance()
        {
            var visited = new HashSet<P>();
            var q = new Queue<P>();
            var s = new Queue<int>();
            q.Enqueue(startPosition);
            s.Enqueue(0);

            while (q.Any())
            {
                var pos = q.Dequeue();
                var dist = s.Dequeue();

                if (pos.Portal == end)
                    return dist;
                if (visited.Contains(pos))
                    continue;

                visited.Add(pos);

                if (pos.Portal > 0 && pos.Portal != start)
                {
                    q.Enqueue(map[pos.OppositePortalY][pos.OppositePortalX]);
                    s.Enqueue(dist + 1);
                }
                for (int i = 0; i < 4; i++)
                {
                    int x2 = -1, y2 = -1;
                    switch (i)
                    {
                        case 0:
                            x2 = pos.X;
                            y2 = pos.Y - 1;
                            break;
                        case 1:
                            x2 = pos.X + 1;
                            y2 = pos.Y;
                            break;
                        case 2:
                            x2 = pos.X;
                            y2 = pos.Y + 1;
                            break;
                        case 3:
                            x2 = pos.X - 1;
                            y2 = pos.Y;
                            break;
                    }
                    if (x2 >= 0 && x2 < map[0].Length && y2 >= 0 && y2 < map.Length && map[y2][x2].Passable)
                    {
                        q.Enqueue(map[y2][x2]);
                        s.Enqueue(dist + 1);
                    }
                }
            }

            return 0;
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D20\\input.txt"))
            {
                string line = "";
                List<string> lines = new List<string>();
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                // create map
                int maxX = lines[0].Length, maxY = lines.Count;
                map = new P[maxY - 4][];
                for (int y = 2; y < maxY - 2; y++)
                {
                    map[y - 2] = new P[maxX - 4];
                    for (int x = 2; x < maxX - 2; x++)
                    {
                        if (lines[y][x] == '.')
                        {
                            int p = 0;

                            if (lines[y - 1][x] >= 'A' && lines[y - 1][x] <= 'Z')
                                p = lines[y - 2][x] * 100 + lines[y - 1][x];
                            else if (lines[y + 1][x] >= 'A' && lines[y + 1][x] <= 'Z')
                                p = lines[y + 1][x] * 100 + lines[y + 2][x];
                            else if (lines[y][x - 1] >= 'A' && lines[y][x - 1] <= 'Z')
                                p = lines[y][x - 2] * 100 + lines[y][x - 1];
                            else if (lines[y][x + 1] >= 'A' && lines[y][x + 1] <= 'Z')
                                p = lines[y][x + 1] * 100 + lines[y][x + 2];

                            map[y - 2][x - 2] = new P() { Passable = true, X = x - 2, Y = y - 2, Portal = p, OppositePortalX = -1, OppositePortalY = -1 };
                        }
                        else
                            map[y - 2][x - 2] = new P() { Passable = false, X = x - 2, Y = y - 2, Portal = 0, OppositePortalX = -1, OppositePortalY = -1 };
                    }
                }

                // find oposite portals
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[y].Length; x++)
                    {
                        if (map[y][x].Portal == start)
                            startPosition = map[y][x];

                        if (map[y][x].Passable && map[y][x].Portal > 0 && map[y][x].Portal != start && map[y][x].Portal != end && map[y][x].OppositePortalX == -1)
                        {
                            int ox = x + 1, oy = y;
                            while (true)
                            {
                                if (ox >= map[y].Length)
                                {
                                    ox = 0;
                                    oy++;
                                }
                                if (map[oy][ox].Portal == map[y][x].Portal)
                                {
                                    map[oy][ox].OppositePortalX = x;
                                    map[oy][ox].OppositePortalY = y;
                                    map[y][x].OppositePortalX = ox;
                                    map[y][x].OppositePortalY = oy;
                                    break;
                                }
                                ox++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(GetDistance());
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}