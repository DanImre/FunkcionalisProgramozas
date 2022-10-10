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
            int alltkertek = int.Parse(s[0]);
            int fajok = int.Parse(s[1]);
            int n = int.Parse(s[2]);

            Dictionary<int, List<(int fajta, int darab)>> x = new Dictionary<int, List<(int fajta, int darab)>>();
            for (int i = 0; i < alltkertek; i++)
                x.Add(i + 1, new List<(int, int)>());

            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x[int.Parse(s[0])].Add((int.Parse(s[1]), int.Parse(s[2])));
            }

            //1, feladat
            Console.WriteLine("#");

            int max = 0;
            int maxI = 0;
            foreach (var item in x)
            {
                int osszeg = 0;
                foreach (var i in item.Value)
                    osszeg += i.darab;

                if(osszeg > max)
                {
                    max = osszeg;
                    maxI = item.Key;
                }
            }

            Console.WriteLine(maxI);

            //2. feladat
            Console.WriteLine("#");

            Dictionary<int, int> fajPerAllatkert = new Dictionary<int, int>();
            for (int i = 0; i < fajok; i++)
                fajPerAllatkert.Add(i + 1, 0);

            foreach (var item in x)
                foreach (var i in item.Value)
                    ++fajPerAllatkert[i.fajta];

            List<int> minIlista = new List<int>() { 1 };
            int min = fajPerAllatkert[1];
            for (int i = 1; i < fajok; i++)
                if (fajPerAllatkert[i + 1] == 0)
                    continue;
                else if (fajPerAllatkert[i + 1] < min)
                {
                    min = fajPerAllatkert[i + 1];
                    minIlista.Clear();
                    minIlista.Add(i + 1);
                }
                else if (fajPerAllatkert[i + 1] == min)
                    minIlista.Add(i + 1);

            Console.WriteLine(minIlista.Count);
            foreach (var item in minIlista)
                Console.Write(item + " ");
            Console.WriteLine();

            //3. feladat
            Console.WriteLine("#");

            int db = 0;
            foreach (var item in x)
            {
                bool van = true;
                foreach (var i in item.Value)
                    if(i.darab < 2)
                    {
                        van = false;
                        break;
                    }

                if (van)
                    ++db;
            }

            Console.WriteLine(db);

            //4. feladat
            Console.WriteLine("#");

            List<(int, int)> nincskozos = new List<(int, int)>();
            for (int i = 0; i < alltkertek; i++)
            {
                bool egyezik = false;
                int hol = 0;
                foreach (var item in x[i + 1])
                {
                    for (int j = i + 1; j < alltkertek; j++)
                    {
                        foreach (var item2 in x[j + 1])
                            if(item.fajta == item2.fajta)
                            {
                                egyezik = true;
                                hol = j + 1;
                                break;
                            }

                        if (egyezik)
                            break;
                    }
                    if (egyezik)
                        break;
                }

                if (!egyezik)
                    nincskozos.Add((i + 1, hol));
            }

            nincskozos.Sort();
            Console.WriteLine(nincskozos[0].Item1 + " " + nincskozos[0].Item2);

            //5. feladat
            Console.WriteLine("#");


        }
    }
}
