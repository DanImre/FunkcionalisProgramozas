using System;
using System.Linq;
using System.Resources;
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
            
            //1
            Console.WriteLine("#");
            Console.WriteLine(x.Where(kk => kk.Item1 <= 0 || kk.Item2 <= 0).Count());
            
            //2
            Console.WriteLine("#");
            int max = 0;
            int maxI = 0;
            for (int i = 0; i < n; i++)
                if(Math.Abs(x[i].Item1 - x[i].Item2) > max)
                {
                    max = Math.Abs(x[i].Item1 - x[i].Item2);
                    maxI = i;
                }

            Console.WriteLine(maxI + 1);

            //3.
            Console.WriteLine("#");
            bool van = false;
            for (int i = 1; i < n; i++)
                if(x[i-1].Item1 > x[i].Item2)
                {
                    Console.WriteLine(i + 1);
                    van = true;
                    break;
                }
            if(!van)
                Console.WriteLine("-1");
            //4.
            Console.WriteLine("#");
            Console.Write(x.Where(kk => kk.Item1 <= 0 && kk.Item2 > 0).Count());
            for (int i = 0; i < n; i++)
                if (x[i].Item1 <= 0 && x[i].Item2 > 0)
                    Console.Write(" " + (i + 1));


        }
    }
}
