using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> x = new List<int>();
            for (int i = 0; i < n; i++)
                x.Add(int.Parse(Console.ReadLine()));

            //1. feladat
            int orzottCounter = 0;
            for (int i = 1; i < x.Count; i++)
                if ((x[i] > 0 && x[i - 1] == 0) || (x[i] == 0 && x[i - 1] > 0))
                    ++orzottCounter;

            Console.WriteLine(orzottCounter);
        }
    }
}
