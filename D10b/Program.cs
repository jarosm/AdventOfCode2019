using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D10b
{
    class Program
    {

        static List<Asteroid> asteroids = new List<Asteroid>();

        public class Asteroid
        {
            public int x;
            public int y;
            public List<Asteroid> visible = null;
            public double angle = 0;

            public Asteroid(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static bool SameAngle(Asteroid from, Asteroid a, Asteroid b)
        {
            if (Math.Sign(a.x - from.x) != Math.Sign(b.x - from.x))
                return false;
            if (Math.Sign(a.y - from.y) != Math.Sign(b.y - from.y))
                return false;
            if (a.x == from.x && b.x == from.x)
                return true;
            if (a.y == from.y && b.y == from.y)
                return true;
            return ((a.x - from.x) * (b.y - from.y) == (b.x - from.x) * (a.y - from.y));
        }

        public static bool LineOfSight(Asteroid from, Asteroid to)
        {
            int dx = Math.Abs(to.x - from.x);
            int dy = Math.Abs(to.y - from.y);
            foreach (Asteroid a in asteroids)
            {
                if (a != from && a != to)
                {
                    int dxa = a.x - from.x;
                    int dya = a.y - from.y;
                    if (Math.Abs(dxa) <= dx && Math.Abs(dya) <= dy && SameAngle(from, to, a))
                        return false;
                }
            }
            return true;
        }

        public static void CountVisible(Asteroid from)
        {
            from.visible = new List<Asteroid>();
            foreach (Asteroid a in asteroids)
                if (a != from && LineOfSight(from, a))
                    from.visible.Add(a);
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D10\\input.txt"))
            {
                string line = "";
                int y = 0;
                while ((line = input.ReadLine()) != null)
                {
                    for (int x = 0; x < line.Length; x++)
                    {
                        if (line[x] == '#')
                            asteroids.Add(new Asteroid(x, y));
                    }
                    y++;
                }
            }

            foreach (Asteroid a in asteroids)
            {
                CountVisible(a);
            }

            Asteroid best = asteroids[0];
            foreach (Asteroid a in asteroids)
                if (a.visible.Count > best.visible.Count)
                    best = a;

            int goal = 200;
            foreach (Asteroid a in best.visible)
            {
                a.angle = -Math.Atan2(a.x - best.x, a.y - best.y);
            }
            best.visible.Sort((a, b) => a.angle.CompareTo(b.angle));
            Asteroid target = best.visible[goal - 1];

            Console.WriteLine(target.x * 100 + target.y);

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
