using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //elso megoldas
            string sorszamok = "";
            string[] s = Console.ReadLine().Split(' ');
            int n = int.Parse(s[0]);
            int k = int.Parse(s[1]);
            int db = 0;
            for (int i = 0; i < n; i++)
            {
                //int kor = int.Parse(Console.ReadLine().Split(' ')[0]);
                if (int.Parse(Console.ReadLine().Split(' ')[0]) < k)
                {
                    sorszamok += " " + (i + 1);
                    ++db;
                }
            }
            Console.WriteLine(db + sorszamok);

        }
    }
}
