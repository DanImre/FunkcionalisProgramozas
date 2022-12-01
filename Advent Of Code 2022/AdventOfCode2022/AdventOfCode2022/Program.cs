using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
    class Program
    {
        public static void Elso()
        {
            List<string> s = File.ReadAllLines("elso.txt").ToList();
            List<int> elfs = new List<int>();
            while (s.Count > 0)
            {
                int temp = 0;
                while (s.Count > 0 && s[0] != "")
                {
                    temp += int.Parse(s[0]);
                    s.RemoveAt(0);
                }
                elfs.Add(temp);
                if (s.Count != 0)
                    s.RemoveAt(0);
            }
            Console.WriteLine(elfs.Max());

            //2. rész:
            elfs.Sort();
            Console.WriteLine(elfs[elfs.Count - 1] + elfs[elfs.Count - 2] + elfs[elfs.Count - 3]);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Melyik feladat: ");
            int n = int.Parse(Console.ReadLine());
            if (n == 1)
                Elso();
        }
    }
}
