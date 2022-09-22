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
            Dictionary<int, string> falszakasz = new Dictionary<int, string>();
            for (int i = 1; i < x.Count; i++)
                if ((x[i] > 0 && x[i - 1] == 0) || (x[i] == 0 && x[i - 1] > 0))
                    falszakasz.Add(i - 1, "orzott");
                else if (x[i] > 0 && x[i - 1] > 0)
                    falszakasz.Add(i - 1, "vedett");
                else
                    falszakasz.Add(i - 1, "semmi");
            /*
            Console.WriteLine();
            Console.WriteLine(falszakasz.Count);*/
            Console.WriteLine(falszakasz.Count(kk => kk.Value == "orzott"));

            //2. feladat
            int db = 0;
            for (int i = 0; i < falszakasz.Count; i++)
                if(falszakasz[i] == "semmi")
                {
                    ++db;
                    ++i;
                }

            Console.WriteLine(db);
            //3. feladat
            for (int i = 0; i < falszakasz.Count; i++)
                if (falszakasz[i] == "semmi")
                {
                    Console.WriteLine((i + 1));
                    break;
                }

            if (falszakasz.Count(kk => kk.Value == "semmi") == 0)
                Console.WriteLine(0);

            //4. feladat
            int? start = null;
            int? end = null;
            for (int i = 0; i < falszakasz.Count; i++)
            {
                int? tempStart = null;
                if (falszakasz[i] == "vedett")
                    tempStart = i;

                while (falszakasz[i] == "vedett" && i < falszakasz.Count)
                    ++i;

                if (tempStart.HasValue)
                    if (!start.HasValue)
                    {
                        start = tempStart;
                        end = i;
                    }
                    else if (end - start < i - tempStart)
                    {
                        start = tempStart;
                        end = i;
                    }
            }

            Console.WriteLine((start + 1) + " " + (end));
        }
    }
}
