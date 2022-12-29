/*
  Készítette: Dán Imre
  Neptun: UUUHSK
  E-mail: uuuhsk@vilaghalo. uuuhsk@INF.ELTE.HU hu uuuhsk@INF.ELTE.HU
  Feladat: Átlagosan legmelegebb település
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static string filename = "be1.txt";

        public static (List<List<int>> x,int n,int m) beovasas()
        {
            int n = 0;
            int m = 0;
            List<List<int>> x = new List<List<int>>();

            if (File.Exists(filename))
            {
                string[] s = File.ReadAllLines(filename);

                foreach (var item in s)
                    Console.WriteLine(item);

                string[] temp = s[0].Split(' ');
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                for (int i = 1; i < s.Length; i++)
                    x.Add(s[i].Split(' ').Select(int.Parse).ToList());
            }
            else
            {
                do
                {
                    string[] s = Console.ReadLine().Split(' ');
                    n = int.Parse(s[0]);
                    m = int.Parse(s[1]);
                    if (n < 1 || m < 1 || n >= 1000 || m >= 1000)
                        Console.Error.WriteLine("Hibás adat!");
                } while (n < 1 || m < 1 || n >= 1000 || m >= 1000);


                for (int i = 0; i < n; i++)
                {
                    List<int> s = new List<int>();
                    bool wrongData = false;
                    do
                    {
                        wrongData = false;
                        string[] temp = Console.ReadLine().Split(' ');
                        foreach (var item in temp)
                        {
                            int bekertszam = 100;
                            if (!int.TryParse(item, out bekertszam) || bekertszam < -50 || bekertszam > 50)
                            {
                                wrongData = true;
                                break;
                            }

                            s.Add(bekertszam);
                        }
                        if (wrongData)
                            Console.Error.WriteLine("Hibás adat!");
                    } while (wrongData);

                    x.Add(s);
                }
            }

            return (x,n,m);
        }

        public static int feladat(List<List<int>> x, int n, int m)
        {
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

            return maxi + 1;
        }

        static void Main(string[] args)
        {
            var temp = beovasas();

            int solution = feladat(temp.x, temp.n, temp.m);

            //Console.WriteLine("Atlagosan legmelegebb nap: " + (maxi + 1));
            Console.WriteLine(solution);
        }
    }
}
