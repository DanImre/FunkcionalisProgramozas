using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Resources;

namespace ElsoOra
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            int.TryParse(Console.ReadLine(),out n);
            int db = 0;
            for (int i = 0; i < n; i++)
                try
                {
                    float temp = float.Parse(Console.ReadLine());
                    if (temp <= 0)
                        ++db;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Számot adjon meg!");
                    --i;
                }

            Console.WriteLine("Fagypont alatti hőmérséklet " + db + " db napon volt.");
        }
    }
}
