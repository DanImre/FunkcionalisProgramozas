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
            int min = x[0].Item1;
            int minI = 0;
            for (int i = 1; i < n; i++)
                if (x[i].Item1 < min)
                {
                    min = x[i].Item1;
                    minI = i;
                }

            Console.WriteLine(minI + 1);

            //2. feladat
            Console.WriteLine(x.Where(kk => kk.Item1 > 1000).Count() != 0 ? x.Where(kk => kk.Item1 > 1000).Max(kk => kk.Item2) : -1);

            //3. feladat
            List<int> arak = new List<int>();
            foreach (var item in x)
                if (!arak.Contains(item.Item2))
                    arak.Add(item.Item2);

            Console.WriteLine(arak.Count);

            //4. feladat
            int counter = 0;
            int max = x[0].Item1;
            string temp = "";
            for (int i = 0; i < x.Count; i++)
                if (x[i].Item1 > max)
                {
                    max = x[i].Item1;
                    ++counter;
                    temp += " " + (i + 1);
                }

            Console.WriteLine(counter + temp);
        }
    }
}
