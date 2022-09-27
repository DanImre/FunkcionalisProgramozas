using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class reszveny
        {
            public reszveny(int sorszam, int kezdoAr, int zaroAr, int legolcsobb, int legdragabb)
            {
                this.sorszam = sorszam;
                this.kezdoAr = kezdoAr;
                this.zaroAr = zaroAr;
                this.legolcsobb = legolcsobb;
                this.legdragabb = legdragabb;
            }

            public int sorszam { get; set; }
            public int kezdoAr { get; set; }
            public int zaroAr { get; set; }
            public int legolcsobb { get; set; }
            public int legdragabb { get; set; }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int napok = int.Parse(s[0]);
            int reszvenyek = int.Parse(s[1]);

            List<List<reszveny>> x = new List<List<reszveny>>();
            for (int i = 0; i < napok; i++)
            {
                int n = int.Parse(Console.ReadLine());
                x.Add(new List<reszveny>());
                for (int j = 0; j < n; j++)
                {
                    s = Console.ReadLine().Split(' ');
                    x[i].Add(new reszveny(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]), int.Parse(s[4])));
                }
            }

            //1. feladat
            Console.WriteLine("#");

            List<int> szamolas = new List<int>();
            foreach (var item in x)
                foreach (var i in item)
                {
                    if (!szamolas.Contains(i.sorszam))
                        szamolas.Add(i.sorszam);
                }

            Console.WriteLine(reszvenyek - szamolas.Count);

            //2. feladat
            Console.WriteLine("#");

            int min = reszvenyek + 1;
            int minI = 0;
            for (int i = 0; i < x.Count; i++)
                if(x[i].Count != 0 && x[i].Count < min)
                {
                    min = x[i].Count;
                    minI = i + 1;
                }

            Console.WriteLine(minI);

            //3. feladat
            Console.WriteLine("#");

            Dictionary<int, int> sorszamMAX = new Dictionary<int, int>();
            Dictionary<int, (bool, int)> mindigNO = new Dictionary<int, (bool, int)>();//4. feladathoz

            for (int i = 0; i < reszvenyek; i++)
            {
                sorszamMAX.Add(i + 1, -1);
                mindigNO.Add(i + 1, (true, 0));
            }

            foreach (var item in x)
                foreach (var i in item)
                {
                    if (sorszamMAX[i.sorszam] < i.legdragabb - i.legolcsobb)
                        sorszamMAX[i.sorszam] = i.legdragabb - i.legolcsobb;

                    if (i.kezdoAr >= i.zaroAr)
                        mindigNO[i.sorszam] = (false, 0);
                    else
                        mindigNO[i.sorszam] = (mindigNO[i.sorszam].Item1, mindigNO[i.sorszam].Item2 + 1);
                }

            min = int.MaxValue;
            List<int> minIk = new List<int>();
            foreach (var item in sorszamMAX)
                if (item.Value != -1 && item.Value < min)
                {
                    min = item.Value;
                    minIk.Clear();
                    minIk.Add(item.Key);
                }
                else if (item.Value == min)
                    minIk.Add(item.Key);

            Console.WriteLine(minIk.Min());
            /*
            Console.WriteLine("///// " + min);
            foreach (var item in sorszamMAX)
                Console.WriteLine("///// " + item.Key + " --- " + item.Value + " //////");*/

            //4. feladat
            Console.WriteLine("#");
            foreach (var item in mindigNO)
                if(item.Value.Item1 && item.Value.Item2 > 0)
                    Console.WriteLine(item.Key);

            //5. feladat
            Console.WriteLine("#");

            Dictionary<int, int> napVeszteseg = new Dictionary<int, int>();
            for (int i = 0; i < napok; i++)
                napVeszteseg.Add(i, 0);

            min = 0;
            minI = -1;
            for (int i = 0; i < x.Count; i++)
            {
                int veszteseg = 0;
                foreach (var item in x[i])
                    veszteseg += item.zaroAr - item.kezdoAr;

                if(min > veszteseg)
                {
                    min = veszteseg;
                    minI = i;
                }
            }

            if (minI == -1)
                Console.WriteLine(-1);
            else
            {
                Console.WriteLine(minI + 1);
                Console.WriteLine(-1*min);
            }

        }
    }
}
