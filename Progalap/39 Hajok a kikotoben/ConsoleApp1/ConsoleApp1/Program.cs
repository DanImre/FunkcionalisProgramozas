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
            int n = int.Parse(s[0]);
            int napok = int.Parse(s[1]);

            List<int> x = new List<int>();
            for (int i = 0; i < n; i++)
                x.Add(int.Parse(Console.ReadLine()));

            //1. feladat
            Console.WriteLine("#");
            Console.WriteLine(napok - x.Distinct().Count());

            //2. feladat
            Console.WriteLine("#");

            List<int> xDistinct = new List<int>(x.Distinct());
            bool volt = false;
            for (int i = 0; i < xDistinct.Count; i++)
                if (!xDistinct.Contains(xDistinct[i] - 1) && !xDistinct.Contains(xDistinct[i] + 1))
                {
                    Console.WriteLine(xDistinct[i]);
                    volt = true;
                    break;
                }

            if (!volt)
                Console.WriteLine(-1);

            //3. feladat
            Console.WriteLine("#");

            int max = 0;
            for (int i = 0; i < xDistinct.Count - 1; i++)
                if (xDistinct[i + 1] - xDistinct[i] - 1 > max)
                    max = xDistinct[i + 1] - xDistinct[i] - 1;

            Console.WriteLine(max);

            //4. feladat
            Console.WriteLine("#");
            Dictionary<int, int> napHajo = new Dictionary<int, int>();
            for (int i = 0; i < x.Count; i++)
                if (napHajo.ContainsKey(x[i]))
                    ++napHajo[x[i]];
                else
                    napHajo.Add(x[i], 1);

            max = 0;
            int maxI = 0;
            foreach (var item in napHajo)
                if(item.Value > max)
                {
                    max = item.Value;
                    maxI = item.Key;
                }

            Console.WriteLine(maxI);

            //5. feladat
            Console.WriteLine("#");
            int startIndex = 0;
            max = 0;
            for (int i = 0; i < xDistinct.Count - 1; i++)
            {
                int? tempstart = null;
                if (xDistinct[i] + 1 == xDistinct[i + 1])
                    tempstart = i;

                while (i < xDistinct.Count - 1 && xDistinct[i] + 1 == xDistinct[i + 1])
                    ++i;

                if (tempstart.HasValue && i - tempstart.Value > max)
                {
                    startIndex = tempstart.Value;
                    max = i - tempstart.Value;
                }
            }

            Console.WriteLine(xDistinct[startIndex] + " " + xDistinct[startIndex + max]);
        }
    }
}
