using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class tanulo
        {
            public tanulo(int tanuloSzam, int sportagSzam)
            {
                this.tanuloSzam = tanuloSzam;
                this.sportagSzam = sportagSzam;
            }

            public int tanuloSzam { get; set; }
            public int sportagSzam { get; set; }

            public bool Equals(tanulo a)
            {
                if (a.tanuloSzam == this.tanuloSzam && a.sportagSzam == this.sportagSzam)
                    return true;

                return false;
            }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int Tanulok = int.Parse(s[0]);
            int Sportagak = int.Parse(s[1]);
            int n = int.Parse(s[2]);

            List<tanulo> x = new List<tanulo>();
            for (int i = 0; i < n; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add(new tanulo(int.Parse(s[0]), int.Parse(s[1])));
            }

            int X = int.Parse(Console.ReadLine());

            //1. feladat
            Console.WriteLine("#");
            Console.WriteLine(x.Count(kk => kk.tanuloSzam == X));

            //2. feladat
            Console.WriteLine("#");

            Dictionary<int, List<int>> sportagTanulo = new Dictionary<int, List<int>>();
            //---------
            //Ezzel 100, nélküle 80
            for (int i = 1; i < Sportagak + 1; i++)
                sportagTanulo.Add(i, new List<int>());
            //---------
            List<int> duppla = new List<int>();

            for (int i = 0; i < x.Count; i++)
            {
                if (!sportagTanulo.ContainsKey(x[i].sportagSzam))
                {
                    sportagTanulo.Add(x[i].sportagSzam, new List<int>() { x[i].tanuloSzam });
                    continue;
                }

                if (sportagTanulo[x[i].sportagSzam].Contains(x[i].tanuloSzam))
                {
                    duppla.Add(x[i].tanuloSzam);
                    x.RemoveAt(i);
                    --i;
                }
                else
                    sportagTanulo[x[i].sportagSzam].Add(x[i].tanuloSzam);
            }
            if (duppla.Count != 0)
                Console.WriteLine(duppla.Min());
            else
                Console.WriteLine(-1);

            //3. feladat
            Console.WriteLine("#");
            for (int i = 1; i < Sportagak + 1; i++)
                Console.Write(sportagTanulo[i].Count + " ");
            Console.WriteLine();

            //4. feladat
            Console.WriteLine("#");
            Dictionary<int, int> szamolas = new Dictionary<int, int>();
            foreach (var item in sportagTanulo)
                foreach (var i in item.Value)
                    if (szamolas.ContainsKey(i))
                        ++szamolas[i];
                    else
                        szamolas.Add(i, 1);

            int max = 0;
            List<int> maxSZAMOK = new List<int>();
            foreach (var item in szamolas)
                if(item.Value > max)
                {
                    max = item.Value;
                    maxSZAMOK.Clear();
                    maxSZAMOK.Add(item.Key);
                }
                else if(item.Value == max)
                    maxSZAMOK.Add(item.Key);

            maxSZAMOK.Sort();
            Console.WriteLine(maxSZAMOK[0]);

            //5. feladat
            Console.WriteLine("#");
            List<(int, int)> egyszerre = new List<(int, int)>(); 
            for (int i = 1; i < Sportagak + 1; i++)
            {
                bool joLesz = true;
                for (int j = i + 1; j < Sportagak + 1; j++)
                {
                    foreach (var item in sportagTanulo[j])
                        if(sportagTanulo[i].Contains(item))
                        {
                            joLesz = false;
                            break;
                        }

                    if (joLesz)
                        egyszerre.Add((i, j));

                    joLesz = true;
                }
            }

            egyszerre.Sort();
            foreach (var item in egyszerre)
                Console.WriteLine(item.Item1 + " " + item.Item2);
        }
    }
}
