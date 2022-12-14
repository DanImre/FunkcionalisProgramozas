﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string negyvenes = "38 43 47 47 44 41 41 36 36 32 34 29 32 35 33 28 28 31 26 23 26 24 19 21 19 20 23 26 27 29 28 30 35 37 41 46 44 46 49 46 48 49 50 50 46 43 43 43 39 37 37 41 37 38 33 35 33 31 29 32 37 36 38 37 42 40 38 41 44 46 41 41 40 41 41 40 43 40 36 38 35 33 36 40 35 31 30 31 31 31 33 28 33 32 33 35 36 33 38 39 36 36 37 32 33 35 40 35 34 31 36 39 40 42 46 41 42 44 49 50 45 45 44 42 46 43 38 36 35 39 34 39 42 44 41 39 37 38 36 38 43 45 49 45 48 50 47 45 40 43 39 42 46 43 39 40 37 40 41 41 37 38 37 41 39 35 33 37 36 35 38 40 44 39 38 37 33 33 36 33 36 37 37 37 39 43 38 40 43 42 47 49 50 48 43 44 45 40 43 42 42 39 35 34 34 32 36 39 41 36 34 34 36 31 28 30 27 22 19 22 21 22 26 30 32 35 30 26 23 26 29 34 32 29 31 26 30 30 32 37 41 42 47 43 38 34 38 38 40 45 49 50 50 45 48 43 43 46 45 47 49 49 50 48 49 50 49 50 49 46 46 43 47 42 37 36 40 42 44 48 50 45 41 43 39 34 32 36 36 32 37 35 36 41 36 34 37 35 40 40 43 48 50 50 50 50 50 48 50 50 45 44 45 48 50 47 42 45 46 50 45 45 41 43 43 48 50 50 50 48 48 50 45 49 45 49 50 50 50 50 45 41 37 40 39 37 32 28 30 28 28 28 26 28 33 34 31 36 39 40 37 40 39 41 43 43 43 40 44 44 45 49 44 42 47 44 47 47 50 46 46 44 46 47 50 48 46 43 43 40 43 42 45 41 45 48 50 50 50 45 50 50 50 50 50 46 50 50 50 50 50 49 50 50 47 48 45 40 45 43 45 44 41 36 32 30 35 40 45 43 40 41 41 45 43 46 42 39 38 43 48 50 50 46 49 50 50 47 46 43 46 45 45 48 50 48 50 50 45 46 42 44 42 44 42 38 41 42 46 46 43 44 40 41 41 44 43 41 44 42 44 42 43 47 49 50 50 49 48 50 48 50 50 47 49 50 50 50 50 49 50 49 46 49 50 45 47 45 43 48 43 48 48 47 48 49 50 50 50 46 50 50 49 50 50 50 50 45 41 40 41 39 37 38 39 37 37 37 36 38 33 38 39 43 46 42 42 40 36 41 43 46 50 49 50 45 43 45 47 49 50 47 44 46 46 41 39 41 46 50 49 47 50 48 45 50 50 50 46 49 48 46 41 41 46 45 42 40 45 43 43 39 41 45 45 47 48 48 50 47 50 50 45 46 47 43 40 44 46 47 43 45 50 50 50 46 50 50 49 50 50 49 44 47 44 45 45 42 42 43 39 39 37 32 31 35 31 35 31 28 27 32 30 30 26 31 27 22 22 19 24 26 25 28 29 29 27 23 21 24 28 29 29 24 28 28 29 30 29 34 36 38 36 37 41 41 42 37 40 40 40 41 46 45 44 41 45 43 39 38 33 31 31 28 25 23 21 22 26 23 19 19 23 26 26 25 23 20 22 22 24 21 25 22 27 29 33 30 25 22 23 22 20 19 18 23 20 19 14 16 15 17 15 13 9 11 6 9 11 16 18 14 15 11 12 14 15 16 15 12 7 7 12 16 14 13 10 7 10 7 11 14 13 8 9 9 6 6 3 1 6 1 2 6 5 7 3 2 4 4 5 6 11 9 7 4 2 0 -3 -5 -7 -2 0 -2 2 -2 2 4 8 11 13 9 5 4 2 4 0 1 4 -1 -6 -5 -1 4 1 5 6 10 10 10 12 16 18 16 17 15 18 14 14 13 13 10 7 10 9 6 9 5 0 0 4 9 9 14 11 8 7 10 7 12 16 19 17 13 14 16 17 13 15 16 18 17 22 17 16 21 19 16 16 12 7 10 10 7 6 1 1 5 8 13 14 10 11 7 8 13 18 16 21 18 22 17 17 13 14 18 13 11 11 15 11 9 7 11 10 12 17 20 22 24 22 22 23 18 13 13 14 16 21 23 21 20 25 23 24 27 22 19 14 10 15 17 14 16 11 7 5 4 4 2 6 3 2 -3 -4 -4 -7 -8 -4 -2 -7 -12 -17 -20 -25 -22 -23 -25 -29 -29 -24 -28 -27 -29 -33 -31 -29 -27 -29 -28 -29 -32 -35 -30 -35 -32 -37 -42 -39 -35 -35 -34 -29 -34 -37 -37 -36 -40 -37 -39 -36 -33 -34 -34 -39 -44 -43 -42 -47 -50";

            string masik = "24 22 23 23 26 27 22 26 29 32 31 34 35 35 32 37 35 31 27 31 36 34 34 36 34 37 36 38 35 32 34 29 31 30 30 30 26 26 29 26 31 34 32 36 34 31 29 26 21 26 25 27 23 26 26 22 23 26 24 28 31 29 30 29 28 29 29 34 33 32 28 23 28 31 36 39 43 44 39 44 40 45 47 45 48 50 50 48 48 50 47 45 46 50 50 47 48 44 43 39 36 34 29 30 28 27 31 28 29 26 31 32 35 33 35 39 38 39 34 30 25 30 29 28 24 28 33 29 25 22 17 13 10 10 6 6 3 1 4 9 8 4 8 12 15 11 7 2 -1 2 5 10 14 11 10 5 7 5 3 8 8 6 9 11 16 17 14 9 14 9 14 11 15 19 16 13 10 13 12 9 9 11 14 15 19 21 18 20 23 18 22 17 19 16 17 22 22 27 27 32 33 38 40 42 39 36 35 30 32 30 35 37 35 31 31 36 34 29 32 33 29 26 27 24 25 28 26 23 21 16 13 18 21 23 25 24 22 26 27 27 31 34 38 42 45 44 41 44 48 50 50 50 48 50 50 46 41 39 42 45 47 47 46 42 39 38 41 45 42 38 43 46 46 49 48 43 47 42 40 43 46 43 42 47 44 47 45 40 42 38 38 41 43 41 36 35 36 36 37 36 35 33 28 23 24 27 28 27 31 28 29 24 19 18 20 18 17 16 20 20 21 25 30 31 34 34 35 36 34 33 29 26 30 27 29 24 19 20 17 14 9 4 9 6 9 6 3 2 0 2 -3 -1 -4 1 1 -2 2 7 6 1 4 1 6 1 3 8 8 8 13 13 18 22 22 22 22 19 18 15 20 24 21 16 11 8 3 0 0 1 -3 -8 -11 -16 -19 -19 -23 -21 -22 -20 -21 -25 -26 -31 -30 -26 -29 -32 -27 -32 -37 -40 -38 -43 -38 -43 -48 -50 -50 -45 -46 -49 -50 -50 -49 -50 -50 -50 -46 -43 -38 -34 -32 -37 -40 -37 -37 -33 -29 -28 -23 -23 -19 -19 -23 -22 -23 -22 -19 -22 -23 -28 -24 -29 -31 -28 -25 -24 -25 -23 -24 -22 -21 -16 -13 -16 -18 -20 -20 -24 -28 -25 -25 -29 -34 -33 -32 -31 -28 -29 -26 -21 -21 -23 -18 -22 -25 -22 -22 -17 -19 -19 -23 -18 -22 -26 -23 -25 -26 -23 -22 -21 -16 -11 -11 -10 -7 -8 -13 -12 -17 -15 -10 -8 -7 -2 -1 -4 -7 -8 -5 -6 -3 -3 -5 -8 -11 -8 -12 -15 -10 -9 -13 -17 -22 -20 -21 -23 -27 -29 -29 -26 -25 -25 -26 -27 -32 -28 -29 -27 -28 -33 -30 -27 -30 -28 -28 -33 -32 -31 -27 -24 -28 -33 -30 -25 -28 -31 -33 -38 -36 -33 -36 -34 -29 -27 -24 -26 -28 -29 -33 -32 -28 -24 -21 -26 -26 -27 -26 -27 -26 -21 -18 -20 -18 -22 -25 -25 -22 -25 -26 -21 -16 -18 -22 -27 -29 -31 -26 -30 -25 -25 -22 -27 -22 -23 -26 -24 -21 -18 -15 -10 -5 0 -1 0 3 5 0 0 4 9 11 6 6 3 1 0 5 7 12 9 9 12 17 22 22 18 21 22 21 24 20 17 13 16 11 7 7 2 4 9 12 14 16 13 16 21 16 20 25 21 22 20 23 23 24 24 29 32 36 35 32 36 33 29 31 27 31 36 39 40 40 37 39 41 38 37 37 41 46 49 45 40 44 42 46 46 45 45 48 50 49 49 50 48 47 48 45 44 49 46 50 50 50 50 45 48 49 50 47 48 48 49 46 49 46 47 48 47 50 50 50 50 50 46 43 46 41 39 34 36 38 43 44 41 36 38 35 30 29 33 37 40 42 40 35 32 33 29 34 30 30 33 29 33 31 30 33 33 35 37 40 38 33 37 35 39 39 39 38 39 44 47 50 45 48 50 50 50 50 48 50 49 50 45 49 46 44 46 41 40 41 41 36 31 29 24 22 21 19 16 16 20 25 22 18 23 26 28 33 38 34 37 36 39 34 36 33 33 38 33 32 37 38 39 42 41 36 34 35 34 35 38 42 43 40 44 41 42 41 44 49 44 39 34 39 44 45 50 47 50 50 50 50 49 50 50 46 50 50 45 46 46 42 47 43 48 48 49 50 47 50 47 48 49 49 45 41 40 44 48 45 40 36 39 42 43 44 39 39 40 40 37 41 39 41 43 46 44 46 49 48 46 44 42 40 45 42 45 40 39 37 33 36 33 34 31 31 36 40 35 40 38 40 45 46 48 47 47 43 42 47 50 48 50 50 50 48 49 50 50 50 48 50 50 50 50 49 48 45 42 43 42 41 40 37 36 36 35 30 30 34 37 37 38 42 38 40 45 46 50 46 47 47 43 47 47 44 42 41 37";

            Console.WriteLine(negyvenes.Count(kk => kk == ' ') + 1);
            Console.WriteLine(masik.Count(kk => kk == ' ') + 1);

            Console.WriteLine(negyvenes.Split(' ').Select(int.Parse).Sum() / 1000.0);
            Console.WriteLine(masik.Split(' ').Select(int.Parse).Sum() / 1000.0);
            */
            //beolvasás
            int n = 0;
            int m = 0;
            do
            {
                string[] s = Console.ReadLine().Split(' ');
                n = int.Parse(s[0]);
                m = int.Parse(s[1]);
                if (n < 1 || m < 1 || n >= 1000 || m >= 1000)
                    Console.Error.WriteLine("Hibás adat!");
            } while (n < 1 || m < 1 || n >= 1000 || m >= 1000);

            List<List<int>> x = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                int[] s;
                do
                {
                    s = Console.ReadLine().Split().Select(int.Parse).ToArray();
                    if(s.Any(kk => kk < -50 || kk > 50))
                        Console.Error.WriteLine("Hibás adat!");
                } while (s.Any(kk => kk < -50 || kk > 50));

                x.Add(s.ToList());
            }

            //feladat

            double max = 0;
            int maxi = -1;
            for (int i = 0; i < n; i++)
            {
                double tempmax = 0;
                foreach (var item in x[i])
                    tempmax += item;

                tempmax /= m;

                if (tempmax <= max && maxi != -1)
                    continue;

                max = tempmax;
                maxi = i;
            }

            //Console.WriteLine("Atlagosan legmelegebb nap: " + (maxi + 1));
            Console.WriteLine((maxi + 1));
        }
    }
}