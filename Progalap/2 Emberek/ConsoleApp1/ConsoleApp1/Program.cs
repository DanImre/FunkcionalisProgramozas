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
            int max = x[0].Item1;
            int maxI = 0;
            for (int i = 1; i < n; i++)
                if (x[i].Item1 > max)
                {
                    max = x[i].Item1;
                    maxI = i;
                }

            Console.WriteLine(maxI + 1);

            //2. feladat
            Console.WriteLine(x.Where(kk => kk.Item1 > 40 && kk.Item2 < 200000).Count());

            //3. feladat
            List<int> eletkorok = new List<int>();
            foreach (var item in x)
                if (!eletkorok.Contains(item.Item1))
                    eletkorok.Add(item.Item1);

            Console.WriteLine(eletkorok.Count);

            //4. feladat
            Console.Write(x.Where(kk => kk.Item1 < 30).Count());
            for (int i = 0; i < x.Count; i++)
                if(x[i].Item1 < 30)
                    Console.Write(" " + (i+1));
        }
    }
}
