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
            int k = int.Parse(s[1]);

            List<int> termekMennyisegek = new List<int>();
            for (int i = 0; i < n; i++)
                termekMennyisegek.Add(int.Parse(Console.ReadLine()));

            List<(int, int)> x = new List<(int, int)>();
            for (int i = 0; i < k; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            int max = 0;
            int maxI = -1;
            for (int i = 0; i < termekMennyisegek.Count; i++)
                if(termekMennyisegek[i] > max)
                {
                    max = termekMennyisegek[i];
                    maxI = i + 1;
                }

            Console.WriteLine(maxI);

            //2. feladaz
            Dictionary<int, int> termekKiszallitas = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
                termekKiszallitas.Add(i + 1, 0);

            foreach (var item in x)
                    termekKiszallitas[item.Item1] += item.Item2;

            List<int> megoldasok = new List<int>();
            for (int i = 0; i < n; i++)
                if (termekMennyisegek[i] == termekKiszallitas[i + 1])
                    megoldasok.Add(i + 1);

            Console.Write(megoldasok.Count + " ");
            foreach (var item in megoldasok)
                Console.Write(item + " ");
            Console.WriteLine();

            //3. feladat

            int db = 0;
            foreach (var item in termekKiszallitas)
                if (item.Value == 0)
                    ++db;

            if (db != 0)
                Console.WriteLine(db);
            else
                Console.WriteLine(-1);

            //4. feladat

            max = 0;
            maxI = -1;
            foreach (var item in termekKiszallitas)
                if (termekMennyisegek[item.Key - 1] - item.Value > max && item.Value != 0)
                {
                    max = termekMennyisegek[item.Key - 1] - item.Value;
                    maxI = item.Key;
                }

            Console.WriteLine(maxI);

            //5. feladat
            foreach (var item in termekKiszallitas.OrderByDescending(kk => kk.Value))
                Console.Write(item.Key + " ");
        }
    }
}
