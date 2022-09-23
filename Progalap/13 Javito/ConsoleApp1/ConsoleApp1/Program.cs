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
            int n = int.Parse(s[0]);
            int k = int.Parse(s[1]);
            int dt = int.Parse(s[2]);

            List<int> x = new List<int>();
            for (int i = 0; i < k; i++)
                x.Add(int.Parse(Console.ReadLine()));

            //1. feladat
            List<int> dbLista = new List<int>();//4 es feladat
            for (int i = 0; i < n + 1; i++)
                dbLista.Add(0);

            foreach (var item in x)
                for (int i = item - dt; i < item + dt; i++)
                    dbLista[i]++;

            for (int i = 1; i < n + 1; i++)
                Console.Write(dbLista[i] + " ");
            Console.WriteLine();

            //2.feladat
            int dtCopy = dt;
            while(dtCopy != 0)
            {
                bool elfernek = true;
                for (int i = 0; i < x.Count - 1; i++)
                    if ((x[i + 1] - dtCopy) - (x[i] + dtCopy) < 0)
                    {
                        elfernek = false;
                        break;
                    }

                if (elfernek)
                    break;

                --dtCopy;
            }

            Console.WriteLine(dtCopy);

            //3. feladat
            List<(int start, int end)> ures = new List<(int start, int end)>();
            for (int i = 1; i < n + 1; i++)
            {
                int? start = null;

                if (dbLista[i] == 0)
                    start = i;

                while (i < n + 1 && dbLista[i] == 0)
                    ++i;

                if(start.HasValue)
                    ures.Add((start.Value, i - 1));

            }

            Console.Write(ures.Count + " ");
            foreach (var item in ures)
                Console.Write(item.start + " " + item.end + " ");
            Console.WriteLine();

            //4. feladat
            for (int i = 0; i < x.Count; i++)
            {
                bool utkozikE = false;
                for (int j = x[i] - dt; j < x[i] + dt; j++)
                    if(dbLista[j] > 1)
                    {
                        utkozikE = true;
                        break;
                    }

                bool eltolhatoE = false;
                int min = 1;
                int max = n;
                if (i != 0)
                    min = x[i - 1] + dt;
                if (i != x.Count)
                    max = x[i + 1] - dt;

                if (max - min >= 2 * dt)
                    eltolhatoE = true;

                if (utkozikE && eltolhatoE)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}
