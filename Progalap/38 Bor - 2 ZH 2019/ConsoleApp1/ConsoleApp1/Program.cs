using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class bor
        {
            public bor(int mennyiseg, int ar)
            {
                this.mennyiseg = mennyiseg;
                this.ar = ar;
            }

            public int mennyiseg { get; set; }
            public int ar { get; set; }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int n = int.Parse(s[0]);
            int K = int.Parse(s[1]);

            List<bor> x = new List<bor>();
            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add(new bor(int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            //Console.WriteLine("#");
            List<int> szamlalas = new List<int>();
            for (int i = 0; i < x.Count; i++)
                if (x[i].mennyiseg > 2000)
                    szamlalas.Add(i + 1);

            Console.Write(szamlalas.Count);
            foreach (var item in szamlalas)
                Console.Write(" " + item);
            Console.WriteLine();

            //2. feladat
            //Console.WriteLine("#");

            bool volte = false;
            for (int i = 0; i < x.Count - 1; i++)
            {
                int szamlalo = 0;

                while (i < x.Count - 1 && x[i].mennyiseg < x[i + 1].mennyiseg)
                {
                    ++i;
                    ++szamlalo;
                }

                if (szamlalo + 1 >= K)
                {
                    Console.WriteLine("IGEN");
                    volte = true;
                    break;
                }
            }

            if(!volte)
                Console.WriteLine("NEM");

            //3. feladat
            //Console.WriteLine("#");
            szamlalas = new List<int>();
            int max = x[0].mennyiseg;
            for (int i = 1; i < x.Count; i++)
                if (x[i].mennyiseg > max)
                {
                    max = x[i].mennyiseg;
                    szamlalas.Add(i + 1);
                }

            Console.Write(szamlalas.Count);
            foreach (var item in szamlalas)
                Console.Write(" " + item);

        }
    }
}
