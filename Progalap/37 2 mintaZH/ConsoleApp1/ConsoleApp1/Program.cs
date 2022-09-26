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
            List<int> szamlalas = new List<int>();
            for (int i = 0; i < n; i++)
                if (x[i].Item2 > 100)
                    szamlalas.Add(i + 1);

            Console.Write(szamlalas.Count);
            foreach (var item in szamlalas)
                Console.Write(" " + item);
            Console.WriteLine();

            //2. feladat
            int max = 0;
            for (int i = 0; i < x.Count; i++)
                if(x[i].Item1 > 100 && max < x[i].Item2)
                    max = x[i].Item2;

            Console.WriteLine(max);

            //3. feladat
            List<int> alapT = new List<int>();
            foreach (var item in x)
                if (!alapT.Contains(item.Item1))
                    alapT.Add(item.Item1);

            Console.WriteLine(alapT.Count);
        }
    }
}
