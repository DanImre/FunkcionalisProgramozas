using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        public class Chip
        {
            public bool JoE { get; set; }
            public bool JolMondE { get; set; }
            public Chip(bool Jo = true, bool jolmond = true)
            {
                this.JoE = Jo;
                this.JolMondE = jolmond;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Elemek száma:");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                ClearCurrentConsoleLine();
                Console.Write("Számot adjon! ");
            }
            List<Chip> chiplist = new List<Chip>();
            for (int i = 0; i < n; i++)
            {
                string[] temp = Console.ReadLine().Split(',');
                if (temp.Length == 2)
                    chiplist.Add(new Chip(temp[0] == "0" ? false : true, temp[1] == "0" ? false : true));
                else
                {
                    --i;
                    ClearCurrentConsoleLine();
                    Console.Write("(,) formátumba adja meg! ");
                }
            }


        }

        public static (bool, bool) CompareChips(Chip a, Chip b)
        {
            (bool, bool) solution = (true, true);
            if (!a.JoE && (b.JoE || (!b.JoE && b.JolMondE)))
                solution = (false, solution.Item2);

            if(!b.JoE && (a.JoE || (!a.JoE && a.JolMondE)))
                solution = (solution.Item1, false);

            return solution;
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop - 1;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
