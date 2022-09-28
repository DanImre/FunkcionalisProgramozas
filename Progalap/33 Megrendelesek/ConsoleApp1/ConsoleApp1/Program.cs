using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class ajanlat
        {
            public ajanlat(int ceg, int termek, int ar)
            {
                this.ceg = ceg;
                this.termek = termek;
                this.ar = ar;
            }

            public int ceg { get; set; }
            public int termek { get; set; }
            public int ar { get; set; }
        }

        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int cegek = int.Parse(s[0]);
            int termekek = int.Parse(s[1]);
            int ajanlatok = int.Parse(s[2]);

            List<ajanlat> x = new List<ajanlat>();
            for (int i = 0; i < ajanlatok; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add(new ajanlat(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
            }

            //1. feladat
            Console.WriteLine("#");
            Dictionary<int, int> termekMaxAr = new Dictionary<int, int>();
            for (int i = 0; i < termekek; i++)
                termekMaxAr.Add(i + 1, 0);

            foreach (var item in x)
                if (termekMaxAr[item.termek] < item.ar)
                    termekMaxAr[item.termek] = item.ar;

            int max = 0;
            int maxI = 0;
            foreach (var item in termekMaxAr)
                if(item.Value > max)
                {
                    max = item.Value;
                    maxI = item.Key;
                }

            Console.WriteLine(maxI);

            //2. feladat
            Console.WriteLine("#");
            Dictionary<int, List<int>> cegTermekekLista = new Dictionary<int, List<int>>();//kell a 4. hez is
            for (int i = 0; i < cegek; i++)
                cegTermekekLista.Add(i + 1, new List<int>());

            foreach (var item in x)
                cegTermekekLista[item.ceg].Add(item.termek);

            max = 0;
            maxI = 0;
            foreach (var item in cegTermekekLista)
                if(item.Value.Count > max)
                {
                    max = item.Value.Count;
                    maxI = item.Key;
                }

            Console.WriteLine(maxI);

            //3. feladat
            Console.WriteLine("#");
            Dictionary<int, int> termekCegDB = new Dictionary<int, int>();
            for (int i = 0; i < termekek; i++)
                termekCegDB.Add(i + 1, 0);

            foreach (var item in x)
                ++termekCegDB[item.termek];

            List<int> megfelelt = new List<int>();
            foreach (var item in termekCegDB)
                if (item.Value == 1)
                    megfelelt.Add(item.Key);

            Console.WriteLine(megfelelt.Count);
            if (megfelelt.Count > 0)
            {
                megfelelt.Sort();
                foreach (var item in megfelelt)
                    Console.Write(item + " ");
                Console.WriteLine();
            }

            //4. feladat
            Console.WriteLine("#");
            bool megvan = false;
            for (int i = 0; i < cegek; i++)
            {
                for (int j = i + 1; j < cegek; j++)
                {
                    bool vegigUgyanaz = true;
                    if (cegTermekekLista[i + 1].Count != cegTermekekLista[j + 1].Count)
                        continue;

                    foreach (var item in cegTermekekLista[j + 1])
                    {
                        if (!cegTermekekLista[i + 1].Contains(item))
                        {
                            vegigUgyanaz = false;
                            break;
                        }
                    }

                    if(vegigUgyanaz)
                    {
                        Console.WriteLine((i + 1) + " " + (j + 1));
                        megvan = true;
                        break;
                    }
                }
                if (megvan)
                    break;
            }

            if(!megvan)
                Console.WriteLine(-1);

            //5. feladat
            Console.WriteLine("#");

            megfelelt = new List<int>();

            for (int i = 0; i < x.Count; i++)//nem biztos hogy rendezett
            {
                int cegSzam = x[i].ceg;

                bool mindegyikKisebb = true;
                while(i < x.Count && x[i].ceg == cegSzam)
                {
                    if (mindegyikKisebb)
                        foreach (var item in x)
                        {
                            if (item.ceg == cegSzam)
                                continue;

                            if (item.termek == x[i].termek && item.ar <= x[i].ar)
                            {
                                mindegyikKisebb = false;
                                break;
                            }
                        }
                    ++i;
                }
                --i;

                if (mindegyikKisebb)
                    megfelelt.Add(cegSzam);
            }

            Console.WriteLine(megfelelt.Count);
            if (megfelelt.Count > 0)
            {
                megfelelt.Sort();
                foreach (var item in megfelelt)
                    Console.Write(item + " ");
                //Console.WriteLine();
            }

        }
    }
}
