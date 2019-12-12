using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D12b
{

    public class Moon
    {
        public (int x, int y, int z) position = (0, 0, 0);
        public (int vx, int vy, int vz) velocity = (0, 0, 0);
    }

    class Program
    {
        static List<Moon> moons = new List<Moon>();

        static void AddStep(int axis)
        {
            for (int i = 0; i < moons.Count - 1; i++)
            {
                for (int j = i + 1; j < moons.Count; j++)
                {
                    switch (axis)
                    {
                        case 0:
                            moons[i].velocity.vx += moons[i].position.x > moons[j].position.x ? -1 : (moons[i].position.x < moons[j].position.x ? 1 : 0);
                            moons[j].velocity.vx += moons[i].position.x > moons[j].position.x ? 1 : (moons[i].position.x < moons[j].position.x ? -1 : 0);
                            break;
                        case 1:
                            moons[i].velocity.vy += moons[i].position.y > moons[j].position.y ? -1 : (moons[i].position.y < moons[j].position.y ? 1 : 0);
                            moons[j].velocity.vy += moons[i].position.y > moons[j].position.y ? 1 : (moons[i].position.y < moons[j].position.y ? -1 : 0);
                            break;
                        case 2:
                            moons[i].velocity.vz += moons[i].position.z > moons[j].position.z ? -1 : (moons[i].position.z < moons[j].position.z ? 1 : 0);
                            moons[j].velocity.vz += moons[i].position.z > moons[j].position.z ? 1 : (moons[i].position.z < moons[j].position.z ? -1 : 0);
                            break;
                    }
                }
            }
            for (int i = 0; i < moons.Count; i++)
            {
                switch (axis)
                {
                    case 0:
                        moons[i].position.x += moons[i].velocity.vx;
                        break;
                    case 1:
                        moons[i].position.y += moons[i].velocity.vy;
                        break;
                    case 2:
                        moons[i].position.z += moons[i].velocity.vz;
                        break;
                }
            }
        }

        static long GetLCM(long a, long b)
        {
            return (a * b) / GetGCD(a, b);
        }

        static long GetGCD(long a, long b)
        {
            while (a != b)
                if (a < b) b = b - a;
                else a = a - b;
            return (a);
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D12\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] temp = line.Substring(1, line.Length - 2).Replace(" ", "").Split(',');
                    moons.Add(new Moon() { position = (Convert.ToInt32(temp[0].Split('=')[1]), Convert.ToInt32(temp[1].Split('=')[1]), Convert.ToInt32(temp[2].Split('=')[1])) });
                }
            }

            // period of X axis
            HashSet<(int, int, int, int, int, int, int, int)> states = new HashSet<(int, int, int, int, int, int, int, int)>();
            int stepx = 0;
            while (true)
            {
                var curXState = (moons[0].position.x, moons[1].position.x, moons[2].position.x, moons[3].position.x, moons[0].velocity.vx, moons[1].velocity.vx, moons[2].velocity.vx, moons[3].velocity.vx);
                if (states.Contains(curXState))
                    break;
                else
                {
                    states.Add(curXState);
                    AddStep(0);
                }
                stepx++;
            }

            states.Clear();
            int stepy = 0;
            while (true)
            {
                var curYState = (moons[0].position.y, moons[1].position.y, moons[2].position.y, moons[3].position.y, moons[0].velocity.vy, moons[1].velocity.vy, moons[2].velocity.vy, moons[3].velocity.vy);
                if (states.Contains(curYState))
                    break;
                else
                {
                    states.Add(curYState);
                    AddStep(1);
                }
                stepy++;
            }

            states.Clear();
            int stepz = 0;
            while (true)
            {
                var curZState = (moons[0].position.z, moons[1].position.z, moons[2].position.z, moons[3].position.z, moons[0].velocity.vz, moons[1].velocity.vz, moons[2].velocity.vz, moons[3].velocity.vz);
                if (states.Contains(curZState))
                    break;
                else
                {
                    states.Add(curZState);
                    AddStep(2);
                }
                stepz++;
            }

            long ans = GetLCM(stepx, GetLCM(stepy, stepz));

            Console.WriteLine(ans);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
