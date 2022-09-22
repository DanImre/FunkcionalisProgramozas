using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<(int, int)> x = new List<(int, int)>();
            for (int i = 0; i < n; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            int max = x[0].Item2;
            int maxI = 0;
            for (int i = 1; i < n; i++)
                if (x[i].Item2 > max)
                {
                    max = x[i].Item2;
                    maxI = i;
                }

            Console.WriteLine(maxI + 1);

            //2. feladat
            Console.WriteLine(x.Where(kk => kk.Item1 > 100 && kk.Item2 < 40).Count());

            //3. feladat
            List<int> alapT = new List<int>();
            foreach (var item in x)
                if (!alapT.Contains(item.Item1))
                    alapT.Add(item.Item1);

            Console.WriteLine(alapT.Count);

            //4. feladat
            Console.Write(x.Where(kk => kk.Item2 > 100).Count());
            for (int i = 0; i < x.Count; i++)
                if (x[i].Item2 > 100)
                    Console.Write(" " + (i + 1));
        }
    }
}
