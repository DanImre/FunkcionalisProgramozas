using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int napok = int.Parse(s[0]);
            int n = int.Parse(s[1]);

            List<int> x = new List<int>();
            for (int i = 0; i < n; i++)
                x.Add(int.Parse(Console.ReadLine()));

            //1. feladat
            Console.WriteLine("#");
            List<int> xDistinct = new List<int>(x.Distinct());
            Console.WriteLine(napok - xDistinct.Count);

            //2. feladat
            Console.WriteLine("#");
            bool volt = false;
            for (int i = 0; i < xDistinct.Count; i++)
                if(xDistinct.Contains(x[i] - 1) && xDistinct.Contains(x[i] + 1))
                {
                    Console.WriteLine(x[i]);
                    volt = true;
                    break;
                }

            if(!volt)
                Console.WriteLine(0);

            //3. feladat
            Console.WriteLine("#");

            int max = xDistinct[0] - 1;
            //int max = 0;
            for (int i = 0; i < xDistinct.Count-1; i++)
                if (xDistinct[i + 1] - xDistinct[i] - 1 > max)
                    max = xDistinct[i + 1] - xDistinct[i] - 1;
            
            if (napok - xDistinct[xDistinct.Count - 1] - 1 > max)
                max = napok - xDistinct[xDistinct.Count - 1] - 1;

            Console.WriteLine(max);

            //4. feladat
            Console.WriteLine("#");
            Dictionary<int, int> napTornado = new Dictionary<int, int>();
            for (int i = 0; i < x.Count; i++)
                if (napTornado.ContainsKey(x[i]))
                    ++napTornado[x[i]];
                else
                    napTornado.Add(x[i], 1);

            Console.WriteLine(napTornado.Max(kk => kk.Value));

            //5. feladat
            Console.WriteLine("#");

            int startIndex = 0;
            max = 0;
            for (int i = 0; i < xDistinct.Count - 1; i++)
            {
                int? tempstart = null;
                if (xDistinct[i] + 1 == xDistinct[i + 1])
                    tempstart = i;

                while(i < xDistinct.Count - 1 && xDistinct[i] + 1 == xDistinct[i + 1])
                    ++i;

                if(tempstart.HasValue && i-tempstart.Value > max)
                {
                    startIndex = tempstart.Value;
                    max = i - tempstart.Value;
                }
            }

            Console.WriteLine(xDistinct[startIndex] + " " + xDistinct[startIndex + max]);
        }
    }
}
