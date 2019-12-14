using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D14a
{
    public class Chemical
    {
        public string name = "";
        public int amountProduced = 0;
        public int leftover = 0;
        public List<Subproduct> inputs = null;
    }

    public class Subproduct
    {
        public Chemical subproduct = null;
        public int amount = 0;
    }

    class Program
    {
        static List<Chemical> chemicals = new List<Chemical>();

        static void ComputeOreAmount(ref Chemical output, int amountNeeded)
        {
            if (output.name == "ORE")
            {
                output.leftover += amountNeeded;
                return;
            }

            int amount = amountNeeded - output.leftover;
            if (amount == 0)
            {
                output.leftover = 0;
                return;
            }
            if (amount < 0)
            {
                output.leftover -= amountNeeded;
                return;
            }
            if (amount > 0)
                output.leftover = 0;

            int mod = amount % output.amountProduced;
            if (mod > 0)
                output.leftover = output.amountProduced - mod;
            for (int i = 0; i < output.inputs.Count; i++)
            {
                ComputeOreAmount(ref output.inputs[i].subproduct, output.inputs[i].amount * (amount / output.amountProduced + (mod > 0 ? 1 : 0)));
            }
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D14\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    line = line.Replace(" => ", ";").Replace(", ", ",");

                    // output
                    string[] t1 = line.Split(';');
                    string[] t2 = t1[1].Split(' ');
                    int index1 = chemicals.FindIndex(a => a.name == t2[1]);
                    if (index1 == -1)
                    {
                        chemicals.Add(new Chemical() { name = t2[1], amountProduced = Convert.ToInt32(t2[0]) });
                        index1 = chemicals.Count - 1;
                    }
                    else
                        chemicals[index1].amountProduced = Convert.ToInt32(t2[0]);

                    // inputs
                    chemicals[index1].inputs = new List<Subproduct>();
                    t2 = t1[0].Split(',');
                    for (int i = 0; i < t2.Length; i++)
                    {
                        t1 = t2[i].Split(' ');
                        int index2 = chemicals.FindIndex(a => a.name == t1[1]);
                        if (index2 == -1)
                        {
                            chemicals.Add(new Chemical() { name = t1[1] });
                            index2 = chemicals.Count - 1;
                        }
                        chemicals[index1].inputs.Add(new Subproduct() { subproduct = chemicals[index2], amount = Convert.ToInt32(t1[0]) });
                    }
                }
            }

            Chemical fuel = chemicals.Find(a => a.name == "FUEL");
            ComputeOreAmount(ref fuel, fuel.amountProduced);

            Console.WriteLine(chemicals.Find(a => a.name == "ORE").leftover);

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}