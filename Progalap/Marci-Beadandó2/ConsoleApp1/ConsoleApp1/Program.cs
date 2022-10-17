using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //beolvasás
            string[] s = Console.ReadLine().Split(' ');
            int n = int.Parse(s[0]);

            List<(int x, int y, int size)> x = new List<(int x, int y, int size)>();
            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
            }
            //beolvasás vége

            //a fő gondolat az hogy az |x1 - x2| (vagyis a 2 x távolsága) az ha kisebb mint a kettő madár területösszege és ez igaz az |y1 - y2| is akkor a 2 téglalap érintkezik
            List<int> solution = new List<int>();
            for (int i = 0; i < x.Count; i++)
            {
                bool egyikkelsembalhezik = true;
                for (int j = i + 1; j < x.Count; j++)
                    if (Math.Abs(x[i].x - x[j].x) <= x[i].size + x[j].size && Math.Abs(x[i].y - x[j].y) <= x[i].size + x[j].size)
                    {
                        egyikkelsembalhezik = false;
                        break;
                    }

                if (egyikkelsembalhezik)
                    solution.Add(i + 1);
            }

            foreach (var item in solution)
                Console.Write(item + " ");
        }
    }
}
