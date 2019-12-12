using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D12a
{
    public class Moon
    {
        public (int x, int y, int z) position = (0, 0, 0);
        public (int vx, int vy, int vz) velocity = (0, 0, 0);
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Moon> moons = new List<Moon>();

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D12\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] temp = line.Substring(1, line.Length - 2).Replace(" ", "").Split(',');
                    moons.Add(new Moon() { position = (Convert.ToInt32(temp[0].Split('=')[1]), Convert.ToInt32(temp[1].Split('=')[1]), Convert.ToInt32(temp[2].Split('=')[1])) });
                }
            }

            int step = 0;
            while (step < 1000)
            {
                for (int i = 0; i < moons.Count - 1; i++)
                {
                    for (int j = i + 1; j < moons.Count; j++)
                    {
                        moons[i].velocity.vx += moons[i].position.x > moons[j].position.x ? -1 : (moons[i].position.x < moons[j].position.x ? 1 : 0);
                        moons[i].velocity.vy += moons[i].position.y > moons[j].position.y ? -1 : (moons[i].position.y < moons[j].position.y ? 1 : 0);
                        moons[i].velocity.vz += moons[i].position.z > moons[j].position.z ? -1 : (moons[i].position.z < moons[j].position.z ? 1 : 0);

                        moons[j].velocity.vx += moons[i].position.x > moons[j].position.x ? 1 : (moons[i].position.x < moons[j].position.x ? -1 : 0);
                        moons[j].velocity.vy += moons[i].position.y > moons[j].position.y ? 1 : (moons[i].position.y < moons[j].position.y ? -1 : 0);
                        moons[j].velocity.vz += moons[i].position.z > moons[j].position.z ? 1 : (moons[i].position.z < moons[j].position.z ? -1 : 0);
                    }
                }

                for (int i = 0; i < moons.Count; i++)
                {
                    moons[i].position.x += moons[i].velocity.vx;
                    moons[i].position.y += moons[i].velocity.vy;
                    moons[i].position.z += moons[i].velocity.vz;
                }

                step++;
            }

            int totalEnergy = 0;
            for (int i = 0; i < moons.Count; i++)
            {
                totalEnergy += (Math.Abs(moons[i].position.x) + Math.Abs(moons[i].position.y) + Math.Abs(moons[i].position.z)) 
                    * (Math.Abs(moons[i].velocity.vx) + Math.Abs(moons[i].velocity.vy) + Math.Abs(moons[i].velocity.vz));
            }

            Console.WriteLine(totalEnergy);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
