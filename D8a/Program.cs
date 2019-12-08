using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8a
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowCount = 6, colCount = 25, layerCount = 0, zeroCount = int.MaxValue, oneCount = 0, twoCount = 0;
            int[][][] image = null;
            
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2019\\D8\\input.txt"))
            {
                string img = "", line = "";
                while ((line = input.ReadLine()) != null)
                {
                    img += line;
                }

                layerCount = img.Length / rowCount / colCount;
                image = new int[layerCount][][];
                int index = 0;
                for (int i = 0; i < layerCount; i++)
                {
                    image[i] = new int[rowCount][];
                    for (int j = 0; j < rowCount; j++)
                    {
                        image[i][j] = new int[colCount];
                        for (int k = 0; k < colCount; k++)
                        {
                            image[i][j][k] = Convert.ToInt32(img[index].ToString());
                            index++;
                        }
                    }
                }
            }

            for (int i = 0; i < layerCount; i++)
            {
                int tempZero = 0, tempOne = 0, tempTwo = 0;
                for (int j = 0; j < rowCount; j++)
                {
                    tempZero += image[i][j].Count(a => a == 0);
                    tempOne += image[i][j].Count(a => a == 1);
                    tempTwo += image[i][j].Count(a => a == 2);
                }
                if (tempZero < zeroCount)
                {
                    zeroCount = tempZero;
                    oneCount = tempOne;
                    twoCount = tempTwo;
                }
            }

            Console.WriteLine((oneCount * twoCount).ToString());
            Console.ReadLine();
        }
    }
}
