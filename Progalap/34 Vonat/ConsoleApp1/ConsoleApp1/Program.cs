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

            List<(int erkezes, int indulas)> x = new List<(int erkezes, int indulas)>();
            List<(int erkezes, int indulas)> all = new List<(int erkezes, int indulas)>();
            for (int i = 0; i < n; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                all.Add((int.Parse(s[0]), int.Parse(s[1])));
                if (s[0] == "0" && s[1] == "0")
                    continue;

                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            Console.WriteLine("#");
            Console.WriteLine(x[x.Count-1].erkezes - x[0].indulas);

            //2. feladat
            Console.WriteLine("#");

            int db = 0;
            foreach (var item in x)
                if (item.erkezes == item.indulas)
                    ++db;

            Console.WriteLine(db);

            //3. feladat
            Console.WriteLine("#");

            if(db == x.Count)
                Console.WriteLine(-2);
            else
            {
                List<int> tobbetVart = new List<int>();
                for (int i = 0; i < all.Count-1; i++)
                {
                    if (all[i].erkezes == all[i].indulas)
                        continue;

                    for (int j = i + 1; j < all.Count; j++)
                    {
                        if (all[j].erkezes == all[j].indulas)
                            continue;

                        if (all[i].indulas - all[i].erkezes < all[j].indulas - all[j].erkezes)
                        {
                            tobbetVart.Add(j + 1);
                            i = j - 1;
                        }
                        break;
                    }
                }

                if (tobbetVart.Count == 0)
                    Console.WriteLine(-1);
                else
                {
                    foreach (var item in tobbetVart)
                        Console.Write(item + " ");
                    Console.WriteLine();
                }
            }

            //4. feladat
            Console.WriteLine("#");

            List<(int, int)> athaladas = new List<(int, int)>();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].erkezes == 0 && all[i].indulas == 0)
                    continue;

                int? start = null;
                if (all[i].erkezes == all[i].indulas)
                {
                    start = i;
                    ++i;
                }

                while (all[i].erkezes == all[i].indulas)
                    ++i;

                if (start.HasValue)
                    athaladas.Add((start.Value + 1, i));
            }

            foreach (var item in athaladas)
                Console.WriteLine(item.Item1 + " " + item.Item2);

            //5. feladat
            Console.WriteLine("#");
            int max = -1;
            (int, int) megoldas = (-1,-1);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].erkezes == 0 || all[i].indulas == 0)
                    continue;

                int? start = null;
                if (all[i].erkezes != all[i].indulas)
                {
                    start = i;
                    ++i;
                }

                while (all[i].erkezes != all[i].indulas)
                    ++i;

                if (start.HasValue && max < i - start.Value)
                {
                    megoldas = (start.Value + 1, i);
                    max = i - start.Value;
                }
            }

            if(megoldas.Item1 == -1)
                Console.WriteLine(-1);
            else
                Console.WriteLine(megoldas.Item1 + " " + megoldas.Item2);
        }
    }
}
