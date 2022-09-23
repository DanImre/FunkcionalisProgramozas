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
            List<List<int>> x = new List<List<int>>();
            Dictionary<int, string> nevek = new Dictionary<int, string>();
            nevek.Add(0, "Zirc");
            nevek.Add(1, "Szentendrei skanzen");
            nevek.Add(2, "Sas-hegy");
            nevek.Add(3, "Kis-Balaton");
            nevek.Add(4, "Margit-sziget");

            for (int i = 0; i < 5; i++)
            {
                List<int> temp = new List<int>();
                string[] s = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++)
                    temp.Add(int.Parse(s[j]));

                x.Add(temp);
            }

            //1. feladat
            for (int i = 0; i < 5; i++)
            {
                int db = 0;
                for (int j = 0; j < x[i].Count; j++)
                    if (x[i][j] != 0)
                        ++db;

                Console.Write(db + " ");
            }
            //egyszerűen nem lehet benne hiba
            Console.WriteLine();

            //2. feladat
            int fomax = 0;
            int fomaxI = 0;
            string nev = nevek[0];
            for (int i = 0; i < 5; i++)
            {
                int max = 0;
                int maxI = 0;
                for (int j = 0; j < x[i].Count; j++)
                    if(x[i][j] > max)
                    {
                        max = x[i][j];
                        maxI = j;
                    }

                if(fomax < max)
                {
                    fomax = max;
                    fomaxI = maxI;
                    nev = nevek[i];
                }
            }

            //Ha mindegyik 0 akkor a legelső település + 1 return-öl
            Console.WriteLine(nev + " " + (fomaxI + 1));

            //3. feladat
            int sum = 0;
            for (int i = 0; i < n; i++)
                sum += Math.Abs(x[0][i] - x[1][i]);

            //egyszerűen nem lehet hiba benne
            Console.WriteLine(sum);

            //4. feladat
            int min = int.MaxValue;
            int minI1 = 0;
            int minI2 = 1;
            for (int i = 0; i < 5; i++) //osszehasonlító alap
            {
                int tempmin = min;
                int tempminI1 = 0;
                int tempminI2 = 0;
                for (int j = 0; j < 5; j++) //mivel hasonlítjuk össze
                {
                    if (i == j)
                        continue;

                    int tempsum = 0;
                    for (int h = 0; h < n; h++)//végigmegyünk az elemeken
                        tempsum += Math.Abs(x[i][h] - x[j][h]);

                    if(tempsum < tempmin)
                    {
                        tempmin = tempsum;
                        tempminI1 = i;
                        tempminI2 = j;
                    }
                }

                if (tempmin < min)
                {
                    min = tempmin;
                    minI1 = tempminI1;
                    minI2 = tempminI2;
                }
            }

            Console.WriteLine(nevek[minI1] + "," + nevek[minI2]);
        }
    }
}
