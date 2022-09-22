using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class kozvetites
        {
            public int ceg { get; set; }
            public int dolgozo { get; set; }
            public int ev { get; set; }
            public int nem { get; set; }

            public kozvetites(int _ceg, int _dolgozo, int _ev, int _nem)
            {
                ceg = _ceg;
                dolgozo = _dolgozo;
                ev = _ev;
                nem = _nem;
            }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int c = int.Parse(s[0]);
            int k = int.Parse(s[1]);

            List<kozvetites> x = new List<kozvetites>();
            for (int i = 0; i < k; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add(new kozvetites(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
            }

            //1. feldat
            Console.WriteLine(x.Count(kk => kk.nem == 0) + " " + x.Count(kk => kk.nem == 1));

            //2. feladat
            Dictionary<int, int> cegEladok = new Dictionary<int, int>();
            foreach (var item in x)
            {
                if (!cegEladok.ContainsKey(item.ceg))
                    cegEladok.Add(item.ceg, 1);
                else
                    ++cegEladok[item.ceg];
            }

            Console.WriteLine(cegEladok.Count);

            //3. feladat
            for (int i = 1; i < c+1; i++)
                if(cegEladok.ContainsKey(i))
                    Console.Write(cegEladok[i] + " ");
                else
                    Console.Write(0 + " ");

            Console.WriteLine();
            //4. feladat
            foreach (var item in x.OrderBy(kk => kk.ev))
                Console.Write(item.dolgozo + " ");

        }
    }
}
