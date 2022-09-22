using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class jelentes
        {
            public int nap { get; set; }
            public int uzlet { get; set; }
            public int db { get; set; }

            public jelentes(int _nap, int _uzlet, int _db)
            {
                nap = _nap;
                uzlet = _uzlet;
                db = _db;
            }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int napok = int.Parse(s[0]); 
            int uzletek = int.Parse(s[1]); 
            int n = int.Parse(s[2]);

            List<jelentes> x = new List<jelentes>();

            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add(new jelentes(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
            }

            //1. feladat
            Console.WriteLine(x.Max(kk => kk.db));

            //2. feladat
            Dictionary<int, int> napEladott = new Dictionary<int, int>();
            foreach (var item in x)
                if (!napEladott.ContainsKey(item.nap))
                    napEladott.Add(item.nap, 1);
                else
                    ++napEladott[item.nap];

            Console.WriteLine(napEladott.Count);

            //3. feladat
            List<int> uzletekAdas = new List<int>();
            for (int i = 0; i < uzletek; i++)
                uzletekAdas.Add(0);

            foreach (var item in x)
                ++uzletekAdas[item.uzlet-1];

            int max = uzletekAdas[0];
            int maxI = 0;
            for (int i = 1; i < uzletekAdas.Count; i++)
                if(max < uzletekAdas[i])
                {
                    max = uzletekAdas[i];
                    maxI = i;
                }

            Console.WriteLine((maxI + 1) + " " + max);

            //4. feladat
            int? start = null;
            int? end = null;
            for (int i = 0; i < napok; i++)
            {
                int? tempStart = null;
                if (napEladott.ContainsKey(i))
                    tempStart = i;

                while (napEladott.ContainsKey(i) && i < napok)
                    ++i;

                if(tempStart.HasValue)
                    if(!start.HasValue)
                    {
                        start = tempStart;
                        end = i;
                    }
                    else if (end - start < i - tempStart)
                    {
                        start = tempStart;
                        end = i;
                    }
            }

            Console.WriteLine(start + " " + (end-1));
        }
    }
}
