using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D24b
{
    class Program
    {
        static List<int[,]> fields = new List<int[,]>();

        static List<int[,]> AdvanceGeneration()
        {
            List<int[,]> temp = new List<int[,]>();
            bool addBelow = false, addUp = false;

            for (int i = 0; i < fields.Count; i++)
            {
                temp.Add(new int[5, 5]);

                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (y == 2 && x == 2)
                            continue;

                        int sum = (x > 0 ? fields[i][y, x - 1] : 0)
                            + (y > 0 ? fields[i][y - 1, x] : 0)
                            + (x < 4 ? fields[i][y, x + 1] : 0)
                            + (y < 4 ? fields[i][y + 1, x] : 0)
                            + (x == 0 && i < fields.Count - 1 ? fields[i + 1][2, 1] : 0)
                            + (x == 4 && i < fields.Count - 1 ? fields[i + 1][2, 3] : 0)
                            + (y == 0 && i < fields.Count - 1 ? fields[i + 1][1, 2] : 0)
                            + (y == 4 && i < fields.Count - 1 ? fields[i + 1][3, 2] : 0)
                            + (x == 2 && y == 1 && i > 0 ? fields[i - 1][0, 0] + fields[i - 1][0, 1] + fields[i - 1][0, 2] + fields[i - 1][0, 3] + fields[i - 1][0, 4] : 0)
                            + (x == 2 && y == 3 && i > 0 ? fields[i - 1][4, 0] + fields[i - 1][4, 1] + fields[i - 1][4, 2] + fields[i - 1][4, 3] + fields[i - 1][4, 4] : 0)
                            + (x == 1 && y == 2 && i > 0 ? fields[i - 1][0, 0] + fields[i - 1][1, 0] + fields[i - 1][2, 0] + fields[i - 1][3, 0] + fields[i - 1][4, 0] : 0)
                            + (x == 3 && y == 2 && i > 0 ? fields[i - 1][0, 4] + fields[i - 1][1, 4] + fields[i - 1][2, 4] + fields[i - 1][3, 4] + fields[i - 1][4, 4] : 0);

                        if (fields[i][y, x] == 1)
                        {
                            if (sum != 1)
                                temp[i][y, x] = 0;
                            else
                                temp[i][y, x] = 1;
                        }
                        else
                        {
                            if (sum == 1 || sum == 2)
                                temp[i][y, x] = 1;
                            else
                                temp[i][y, x] = 0;
                        }

                        if (temp[i][y, x] == 1)
                        {
                            if (i == fields.Count - 1 && (y == 0 || y == 4 || x == 0 || x == 4))
                                addUp = true;
                            if (i == 0 && ((x == 2 && y == 1) || (x == 2 && y == 3) || (x == 1 && y == 2) || (x == 3 && y == 2)))
                                addBelow = true;
                        }
                    }
                }
            }

            if (addBelow)
                temp.Insert(0, new int[5, 5]);
            if (addUp)
                temp.Add(new int[5, 5]);

            return temp;
        }

        static void Main(string[] args)
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D24\\input.txt"))
            {
                string line = "";
                fields.Add(new int[5, 5]);
                int c = 0;
                while ((line = input.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                        fields[0][c, i] = line[i] == '.' ? 0 : 1;
                    c++;
                }
            }

            fields.Insert(0, new int[5, 5]);
            fields.Add(new int[5, 5]);

            for (int i = 0; i < 200; i++)
            {
                fields = AdvanceGeneration();
            }

            int sum = 0;
            for (int i = 0; i < fields.Count; i++)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        sum += fields[i][y, x];
                    }
                }
            }

            Console.WriteLine(sum);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}