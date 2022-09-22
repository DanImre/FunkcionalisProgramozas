using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int napok = int.Parse(s[0]);
            int n = int.Parse(s[1]);
            List<(int, int)> x = new List<(int, int)>();
            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            Console.WriteLine(x.Max(kk => kk.Item2 - kk.Item1 + 1));

            //2. feladat
            List<int> napLista = new List<int>();
            for (int i = 0; i < napok; i++)
                napLista.Add(0);

            foreach (var item in x)
                for (int i = item.Item1 - 1; i < item.Item2; i++)
                    ++napLista[i];

            foreach (var item in napLista)
                Console.Write(item + " ");

            Console.WriteLine();

            //3. feladat
            int max = napLista[0];
            int maxI = 0;
            for (int i = 1; i < napLista.Count; i++)
                if(napLista[i] > max)
                {
                    max = napLista[i];
                    maxI = i;
                }

            Console.WriteLine(maxI + 1);

            //4. feladat
            int? start = null;
            int? end = null;
            for (int i = 0; i < napLista.Count; i++)
            {
                int? currentStart = null;
                if (napLista[i] < 2)
                    currentStart = i;

                while (i < napLista.Count && napLista[i] < 2)
                    ++i;

                if (currentStart.HasValue)
                    if(!start.HasValue)
                    {
                        start = currentStart.Value;
                        end = i;
                    }
                    else if (end - start < i - currentStart.Value)
                    {
                        start = currentStart.Value;
                        end = i;
                    }
            }
            if (!start.HasValue)
                Console.WriteLine(0);
            else
                Console.WriteLine((start + 1) + " " + end);
        }
    }
}
