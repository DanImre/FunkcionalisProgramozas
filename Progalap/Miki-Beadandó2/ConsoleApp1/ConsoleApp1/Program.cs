using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            byte n = byte.Parse(Console.ReadLine());//itt indítjuk mert itt olvasunk be eloszor
            stopwatch.Start();
            byte db = 0;
            for (byte i = 0; i < n; i++)
            {
                if (byte.Parse(Console.ReadLine()) < 10)
                    ++db;
            }
            Console.Write(db + " Ido: " + stopwatch.Elapsed);

        }
    }
}
