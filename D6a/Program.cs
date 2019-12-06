using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6a
{
    class Program
    {
        public class Body
        {
            public string Name = "";
            public List<Body> Childs = new List<Body>();
        }

        static List<string> inputData = new List<string>();
        static List<Body> bodies = new List<Body>();

        static void BuildTree(Body parent)
        {
            int index = inputData.FindIndex(a => a.StartsWith(parent.Name));
            while (index > -1)
            {
                parent.Childs.Add(new Body() { Name = inputData[index].Substring(4) });
                inputData.RemoveAt(index);
                index = inputData.FindIndex(a => a.StartsWith(parent.Name));
            }

            for (int i = 0; i < parent.Childs.Count; i++)
            {
                BuildTree(parent.Childs[i]);
            }
        }

        static int CountTree(Body parent, int level)
        {
            int count = (level + 1) * parent.Childs.Count;
            for (int i = 0; i < parent.Childs.Count; i++)
            {
                count += CountTree(parent.Childs[i], level + 1);
            }
            return count;
        }

        static void Main(string[] args)
        {
            int count = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D6\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    inputData.Add(line);
                }

                bodies.Add(new Body() { Name = "COM" });
                BuildTree(bodies[0]);

                count = CountTree(bodies[0], 0);
            }

            Console.WriteLine(count.ToString());
            Console.ReadLine();
        }
    }
}
