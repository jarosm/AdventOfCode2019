using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2a
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D2\\input.txt"))
            {
                string prog = "";
                if ((prog = input.ReadLine()) != null)
                {
                    try
                    {
                        string[] steps = prog.Split(',');
                        int index = 0;
                        bool end = false;

                        while (!end && (index < steps.Length))
                        {
                            switch (steps[index])
                            {
                                case "1":
                                    steps[Convert.ToInt32(steps[index + 3])] = (Convert.ToInt32(steps[Convert.ToInt32(steps[index + 1])]) + Convert.ToInt32(steps[Convert.ToInt32(steps[index + 2])])).ToString();
                                    break;
                                case "2":
                                    steps[Convert.ToInt32(steps[index + 3])] = (Convert.ToInt32(steps[Convert.ToInt32(steps[index + 1])]) * Convert.ToInt32(steps[Convert.ToInt32(steps[index + 2])])).ToString();
                                    break;
                                case "99":
                                    end = true;
                                    break;
                                default:
                                    end = true;
                                    break;
                            }

                            index += 4;
                        }

                        Console.WriteLine(steps[0]);
                    }
                    catch { }
                }
            }
            Console.ReadLine();
        }
    }
}
