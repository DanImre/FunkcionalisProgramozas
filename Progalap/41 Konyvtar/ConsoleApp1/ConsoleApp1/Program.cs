using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class beszerzes
        {
            public beszerzes(int konyvkod, int temakod, int beszerzesEve, int db)
            {
                this.konyvkod = konyvkod;
                this.temakod = temakod;
                this.beszerzesEve = beszerzesEve;
                this.db = db;
            }

            public int konyvkod { get; set; }
            public int temakod { get; set; }
            public int beszerzesEve { get; set; }
            public int db { get; set; }
        }
            static void Main(string[] args)
            {
                int n = int.Parse(Console.ReadLine());
                List<beszerzes> x = new List<beszerzes>();
                for (int i = 0; i < n; i++)
                {
                    string[] s = Console.ReadLine().Split(' ');
                    x.Add(new beszerzes(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
                }

                //1. feladat
                Console.WriteLine("#");
                foreach (var item in x)
                    if(item.db == 1)
                    {
                        Console.WriteLine(item.beszerzesEve);
                        break;
                    }
                
                //2. feladat
                Console.WriteLine("#");
                List<int> evek = new List<int>();

                foreach (var item in x)
                    if (!evek.Contains(item.beszerzesEve))
                        evek.Add(item.beszerzesEve);

                Console.WriteLine(evek.Count);
                foreach (var item in evek)
                    Console.Write(item + " ");
                Console.WriteLine();

                //3. feladat
                Console.WriteLine("#");

                Dictionary<int, int> konyvek = new Dictionary<int, int>();
                foreach (var item in x)
                    if (konyvek.ContainsKey(item.konyvkod))
                        ++konyvek[item.konyvkod];
                    else
                        konyvek.Add(item.konyvkod, 1);

                int max = 0;
                int maxI = 0;
                foreach (var item in konyvek)
                    if(item.Value > max)
                    {
                        max = item.Value;
                        maxI = item.Key;
                    }

                Console.WriteLine(maxI);

                //4. feladat
                Console.WriteLine("#");
                max = 1;
                maxI = -1;
                for (int i = 1; i < x.Count; i++)
                    if(x[i].beszerzesEve - x[i-1].beszerzesEve > max)
                    {
                        max = x[i].beszerzesEve - x[i - 1].beszerzesEve;
                        maxI = i;
                    }

                if (maxI == -1)
                    Console.WriteLine("-1 -1");
                else
                    Console.WriteLine((x[maxI - 1].beszerzesEve + 1) + " " + (x[maxI].beszerzesEve - 1));

                //5. feladat
                Console.WriteLine("#");

                Dictionary<int, int> temakod = new Dictionary<int, int>();
                foreach (var item in x)
                    if (temakod.ContainsKey(item.temakod))
                        temakod[item.temakod] += item.db;
                    else
                        temakod.Add(item.temakod, item.db);

                Console.WriteLine(temakod.Count);
                foreach (var item in temakod)
                    Console.WriteLine(item.Key + " " + item.Value);
            }
    }
}
