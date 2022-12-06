using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] n = Console.ReadLine().Split(' ').Select(kk => int.Parse(kk)).ToArray();

            List<List<int>> x = new List<List<int>>();

            for (int i = 0; i < n[0]; i++)
                x.Add(Console.ReadLine().Split(' ').Select(kk => int.Parse(kk)).ToList());

            double max = 0;
            int maxi = -1;
            for (int i = 0; i < n[0]; i++)
            {
                double tempmax = 0;
                foreach (var item in x[i])
                    tempmax += item / (double)n[1];

                if (tempmax <= max && maxi != -1)
                    continue;

                max = tempmax;
                maxi = i;
            }

            //Console.WriteLine("Atlagosan legmelegebb nap: " + (maxi + 1));
            Console.WriteLine((maxi + 1));
        }
    }
}
