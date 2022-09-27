using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class szuletes
        {
            public int nap { get; set; }
            public int honap { get; set; }
            public int db { get; set; }

            public szuletes(int _honap, int _nap, int _db)
            {
                this.honap = _honap;
                this.nap = _nap;
                this.db = _db;
            }

        }
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<szuletes> x = new List<szuletes>();
            for (int i = 0; i < n; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                x.Add(new szuletes(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
            }

            //1. feladat
            Console.WriteLine(x.Sum(kk => kk.db));

            //2. feladat
            int max = 0;
            int maxI = 0;
            for (int i = 1; i < x.Count; i++)
                if(x[i].db - x[i - 1].db >= max)
                {
                    max = x[i].db - x[i - 1].db;
                    maxI = i;
                }

            if (max == 0)
                Console.WriteLine(0);
            else
                Console.WriteLine(x[maxI].honap + " " + x[maxI].nap);

            //3. feladat
            int index = 1;
            while (index < 13)
            {
                if(x.Count(kk => kk.honap == index) == 0)
                {
                    Console.WriteLine("IGEN " + index);
                    break;
                }

                ++index;
            }
            if(index == 13)
                Console.WriteLine("NEM");

            //4. feladat
            List<(int start, int end)> startEnd = new List<(int start, int end)>();
            for (int i = 0; i < x.Count - 1; i++)
            {
                int? tempStart = null;
                if (x[i].db < x[i + 1].db)
                    tempStart = i;

                while (i < x.Count-1 && x[i].db < x[i + 1].db)
                    ++i;

                if (tempStart.HasValue && i - tempStart >= 1)
                    startEnd.Add((tempStart.Value, i));
            }

            Console.Write(startEnd.Count + " ");
            foreach (var item in startEnd)
                Console.Write(x[item.start].honap + " " + x[item.start].nap + " " + x[item.end].honap + " " + x[item.end].nap + " ");
        }
    }
}
