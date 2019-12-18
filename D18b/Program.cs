using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D18b
{
    class Program
    {
        static List<string> map = new List<string>();

        private struct P : IEquatable<P>
        {
            public int X { get; set; }

            public int Y { get; set; }

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

            public IEnumerable<P> Around()
            {
                yield return new P { X = X, Y = Y - 1 };
                yield return new P { X = X - 1, Y = Y };
                yield return new P { X = X + 1, Y = Y };
                yield return new P { X = X, Y = Y + 1 };
            }
        }

        private struct State
        {
            public PSet Positions { get; set; }

            public int OwnedKeys { get; set; }

            public int Steps { get; set; }
        }

        private struct PSet : IEquatable<PSet>
        {
            public P P1 { get; set; }

            public P P2 { get; set; }

            public P P3 { get; set; }

            public P P4 { get; set; }

            public P this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 1:
                            return P1;
                        case 2:
                            return P2;
                        case 3:
                            return P3;
                        case 4:
                            return P4;
                    }
                    return P1;
                }
                set
                {
                    switch (index)
                    {
                        case 1:
                            P1 = value;
                            break;
                        case 2:
                            P2 = value;
                            break;
                        case 3:
                            P3 = value;
                            break;
                        case 4:
                            P4 = value;
                            break;
                    }
                }
            }

            public PSet Clone()
            {
                return new PSet
                {
                    P1 = P1,
                    P2 = P2,
                    P3 = P3,
                    P4 = P4
                };
            }

            public bool Equals(PSet other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(P1, other.P1) && Equals(P2, other.P2) && Equals(P3, other.P3) && Equals(P4, other.P4);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((PSet)obj);
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 17;
                    // Suitable nullity checks etc, of course :)
                    hash = hash * 23 + P1.GetHashCode();
                    hash = hash * 23 + P2.GetHashCode();
                    hash = hash * 23 + P3.GetHashCode();
                    hash = hash * 23 + P4.GetHashCode();
                    return hash;
                }
            }
        }

        private class ReachableKey
        {
            public char Key { get; set; }

            public int Distance { get; set; }

            public int Obstacles { get; set; }
        }

        private static P FindPositionOf(char c, string[] map)
        {
            var startingLine = map.Single(_ => _.Contains(c));
            var startingColumn = startingLine.IndexOf(c);
            var startingRow = Array.IndexOf(map, startingLine);

            return new P { X = startingColumn, Y = startingRow };
        }

        private static List<ReachableKey> ReachableKeys(string[] map, P start, string currentKeys)
        {
            var list = new List<ReachableKey>();
            var visited = new HashSet<P>();

            var q = new Queue<P>();
            var s = new Queue<int>();
            var o = new Queue<int>();
            q.Enqueue(start);
            s.Enqueue(0);
            o.Enqueue(0);

            while (q.Any())
            {
                var pos = q.Dequeue();
                var dist = s.Dequeue();
                var obst = o.Dequeue();

                if (visited.Contains(pos))
                {
                    continue;
                }

                visited.Add(pos);

                var c = map[pos.Y][pos.X];

                if (c == '@' || c == '1' || c == '2' || c == '3' || c == '4')
                {
                    c = '.';
                }

                if (char.IsLower(c))
                {
                    list.Add(new ReachableKey { Distance = dist, Key = c, Obstacles = obst });

                    foreach (var p in pos.Around())
                    {
                        q.Enqueue(p);
                        s.Enqueue(dist + 1);
                        o.Enqueue(obst);
                    }
                }
                else if (char.IsUpper(c))
                {
                    foreach (var p in pos.Around())
                    {
                        q.Enqueue(p);
                        s.Enqueue(dist + 1);
                        o.Enqueue(obst |= (int)Math.Pow(2, (char.ToLower(c) - 'a')));
                    }
                }
                else if (c == '.')
                {
                    foreach (var p in pos.Around())
                    {
                        q.Enqueue(p);
                        s.Enqueue(dist + 1);
                        o.Enqueue(obst);
                    }
                }
            }

            return list;
        }

        private static int CollectKeys(string[] map, Dictionary<P, List<ReachableKey>> keyPaths, Dictionary<char, P> positions, char[] robots)
        {
            var pos = robots.Select(c => FindPositionOf(c, map)).ToArray();
            var currentMinimum = int.MaxValue;

            var startingSet = new PSet();
            for (var index = 0; index < pos.Length; index++)
            {
                var p = pos[index];
                startingSet[index + 1] = p;
            }

            var q = new Queue<State>();
            q.Enqueue(new State { Positions = startingSet, OwnedKeys = 0 });

            var visited = new Dictionary<(PSet, int), int>();
            var finishValue = 0;
            for (var i = 0; i < positions.Count; ++i)
            {
                finishValue |= (int)Math.Pow(2, i);
            }

            while (q.Any())
            {
                var state = q.Dequeue();

                var valueTuple = (state.Positions, state.OwnedKeys);
                if (visited.TryGetValue(valueTuple, out var steps))
                {
                    if (steps <= state.Steps)
                    {
                        continue;
                    }

                    // this is the crucial bit
                    // if the current state is a better path to a known state, update -
                    // this will cull more future paths, leading to faster convergence
                    visited[valueTuple] = state.Steps;
                }
                else
                {
                    visited.Add(valueTuple, state.Steps);
                }

                if (state.OwnedKeys == finishValue)
                {
                    currentMinimum = Math.Min(currentMinimum, state.Steps);
                    continue;
                }

                for (int i = 1; i <= robots.Length; i++)
                {
                    foreach (var k in keyPaths[state.Positions[i]])
                    {
                        var ki = (int)Math.Pow(2, k.Key - 'a');
                        if ((state.OwnedKeys & ki) == ki || (k.Obstacles & state.OwnedKeys) != k.Obstacles)
                        {
                            continue;
                        }

                        var newOwned = state.OwnedKeys | ki;

                        var newPos = state.Positions.Clone();
                        newPos[i] = positions[k.Key];
                        q.Enqueue(new State
                        {
                            Positions = newPos,
                            OwnedKeys = newOwned,
                            Steps = state.Steps + k.Distance
                        });
                    }
                }
            }

            return currentMinimum;
        }

        private static Dictionary<char, P> GetPositions(string[] map, List<char> keys)
        {
            var dict = new Dictionary<char, P>();

            foreach (var k in keys)
            {
                dict.Add(k, FindPositionOf(k, map));
            }

            return dict;
        }


        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D18\\input2.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    map.Add(line);
                }
            }

            var keys = map.SelectMany(_ => _.Where(char.IsLower)).ToList();

            var dictionary = new Dictionary<P, List<ReachableKey>>();

            for (var i = '1'; i <= '4'; i++)
            {
                dictionary[FindPositionOf(i, map.ToArray())] = ReachableKeys(map.ToArray(), FindPositionOf(i, map.ToArray()), string.Empty);
            }
            
            foreach (var k in keys)
            {
                dictionary[FindPositionOf(k, map.ToArray())] = ReachableKeys(map.ToArray(), FindPositionOf(k, map.ToArray()), string.Empty);
            }

            var minimumSteps = CollectKeys(map.ToArray(), dictionary, GetPositions(map.ToArray(), keys), new[] { '1', '2', '3', '4' });

            Console.WriteLine(minimumSteps);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
