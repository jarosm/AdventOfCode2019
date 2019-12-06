using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6b
{
    class Program
    {
        public class Body
        {
            public string Name = "";
            public Body Parent = null;
            public List<Body> Childs = new List<Body>();
        }

        static List<string> inputData = new List<string>();
        static List<Body> bodies = new List<Body>();
        static Body you = null;
        static Body santa = null;

        static void BuildTree(Body parent)
        {
            int index = inputData.FindIndex(a => a.StartsWith(parent.Name));
            while (index > -1)
            {
                parent.Childs.Add(new Body() { Name = inputData[index].Substring(4), Parent = parent });
                inputData.RemoveAt(index);
                index = inputData.FindIndex(a => a.StartsWith(parent.Name));
            }

            for (int i = 0; i < parent.Childs.Count; i++)
            {
                if (parent.Childs[i].Name == "YOU")
                    you = parent.Childs[i];
                if (parent.Childs[i].Name == "SAN")
                    santa = parent.Childs[i];

                BuildTree(parent.Childs[i]);
            }
        }

        static List<string> GetPath(Body child)
        {
            List<string> path = new List<string>();
            Body parent = child.Parent;
            while (parent != null)
            {
                path.Insert(0, parent.Name);
                parent = parent.Parent;
            }
            return path;
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

                List<string> pathToYou = GetPath(you);
                List<string> pathToSanta = GetPath(santa);
                while (pathToYou[0] == pathToSanta[0])
                {
                    pathToYou.RemoveAt(0);
                    pathToSanta.RemoveAt(0);
                }
                count = pathToYou.Count + pathToSanta.Count;
            }

            Console.WriteLine(count.ToString());
            Console.ReadLine();
        }
    }
}
