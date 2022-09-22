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
            int ossz = int.Parse(s[0]);
            int n = int.Parse(s[1]);
            List<(int, int)> x = new List<(int, int)>();
            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            /*
            int min = x[0].Item2 - x[0].Item1;
            for (int i = 1; i < n; i++)
                if (x[i].Item2 - x[i].Item1 < min)
                    min = x[i].Item2 - x[i].Item1;

            Console.WriteLine(min);*/
            Console.WriteLine(x.Min(kk => kk.Item2 - kk.Item1));

            //2. feladat
            List<int> ut = new List<int>();
            for (int i = 0; i < ossz; i++) //+1 ?
                ut.Add(0);

            foreach (var item in x)
                for (int i = item.Item1; i < item.Item2; i++)
                    ++ut[i];

            bool meglett = false;
            for (int i = 0; i < ut.Count; i++)
                if (ut[i] >= 3)
                {
                    Console.WriteLine(i);
                    meglett = true;
                    break;
                }

            if(!meglett)
                Console.WriteLine(-1);

            //3. feladat
            Console.WriteLine(ut.Where(kk => kk == 0).Count());

            //4. feladat
            int max = 0;
            int counter = 0;
            for (int i = 0; i < ut.Count; i++)
            {
                if (ut[i] != 0)
                {
                    if (counter > max)
                        max = counter;
                    counter = 0;
                }
                else
                    ++counter;
            }
            if (counter > max)
                max = counter;

            Console.WriteLine(max);
        }
    }
}
