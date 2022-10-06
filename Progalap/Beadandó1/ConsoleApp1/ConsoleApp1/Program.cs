using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<(int, int)> x = new List<(int, int)>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            Console.WriteLine(x.Count(kk => kk.Item1 > 100 && kk.Item2 < 40));

            /* Avagy:
            int db = 0;
            foreach (var item in x)
                if (item.Item1 > 100 && item.Item2 < 40)
                    ++db;
            Console.WriteLine(db);
            */
        }
    }
}
