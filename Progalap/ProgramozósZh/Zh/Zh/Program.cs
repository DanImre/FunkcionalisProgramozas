using System;
using System.Linq;
using System.Collections.Generic;

namespace Zh
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> szamok = new List<int>();
            for (int i = 0; i < n; i++)
                szamok.Add(int.Parse(Console.ReadLine()));

            List<(int kezdoIndex, int vegIndex, int sum)> solutions = new List<(int kezdoIndex, int vegIndex, int sum)>();
            int start = -1;
            int end = 0;
            int sum = 0;
            //lehetséges sorozatok kiválasztása
            for (int i = 0; i < szamok.Count-1; i++)
            {
                if (szamok[i] < szamok[i + 1])
                {
                    end = i + 1;

                    if (start == -1)
                    {
                        start = i;
                        sum = szamok[i] + szamok[i + 1];
                    }
                    else
                        sum += szamok[i + 1];
                }
                else
                {
                    solutions.Add((start, end, sum));
                    start = -1;
                }

                if(i == szamok.Count-2 && szamok[i] < szamok[i + 1])
                    solutions.Add((start, end, sum));
            }

            //Maximum keresés
            (int kezdoIndex, int vegIndex, int sum) maxElem = (-1, 0, 0);
            foreach (var item in solutions)
            {
                if (item.kezdoIndex == item.vegIndex || item.kezdoIndex == 0 || item.vegIndex == szamok.Count - 1) //nem megfelelő sorok
                    continue;

                if (maxElem.sum < item.sum)
                    maxElem = item;
            }

            if (maxElem.kezdoIndex == -1)
                Console.WriteLine("Nincs ilyen sorozat.");
            else
            {
                Console.WriteLine("(0 ás indexelésben) Kezdőindexe: " + maxElem.kezdoIndex + ". | Végindexe " + maxElem.vegIndex + ". | Abszolút értékeinek összege: " + maxElem.sum);
                Console.WriteLine("(1 es indexelésben) Kezdőindexe: " + (maxElem.kezdoIndex + 1) + ". | Végindexe " + (maxElem.vegIndex + 1) + ". | Abszolút értékeinek összege: " + maxElem.sum);
            }
        }
    }
}
