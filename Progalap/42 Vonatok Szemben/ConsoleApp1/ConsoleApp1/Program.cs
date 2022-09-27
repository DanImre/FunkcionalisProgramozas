using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        class megallo
        {
            public int varKUKU { get; set; }
            public int varPIPI { get; set; }
            public int balraIDO { get; set; }
            public int jobbraIDO { get; set; }

            public megallo(int _varKUKU, int _varPIPI, int _balraIDO, int _jobbraIDO)
            {
                this.varKUKU = _varKUKU;
                this.varPIPI = _varPIPI;
                this.balraIDO = _balraIDO;
                this.jobbraIDO = _jobbraIDO;
            }

            public megallo()
            {
                this.varKUKU = 0;
                this.varPIPI = 0;
                this.balraIDO = 0;
                this.jobbraIDO = 0;
            }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int K_ind = int.Parse(s[0]);
            int P_ind = int.Parse(s[1]);
            int n = int.Parse(s[2]);

            List<megallo> x = new List<megallo>();
            for (int i = 0; i < n + 2; i++)
                x.Add(new megallo());

            s = Console.ReadLine().Split(' ');
            x[0].jobbraIDO = int.Parse(s[0]);
            x[0].varKUKU = K_ind;
            x[x.Count - 1].balraIDO = int.Parse(s[s.Length - 1]);
            x[x.Count - 1].varPIPI = P_ind;
            for (int i = 1; i < n+1; i++)
            {
                x[i].jobbraIDO = int.Parse(s[i]);
                x[i].balraIDO = int.Parse(s[i - 1]);
            }

            s = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
                x[i + 1].varKUKU = int.Parse(s[i]);

            s = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
                x[i + 1].varPIPI = int.Parse(s[i]);

            //1. feladat
            Console.WriteLine("#");
            Console.WriteLine(x.Count(kk => kk.varKUKU == 0 && kk.varPIPI == 0));

            //2. feladat
            Console.WriteLine("#");
            int sumK = 0;
            for (int i = 0; i < x.Count; i++)
                sumK += x[i].varKUKU + x[i].jobbraIDO;

            int sumP = 0;
            for (int i = x.Count-1; i >= 0; i--)
                sumP += x[i].varPIPI + x[i].balraIDO;

            Console.WriteLine(sumK + " " + sumP);

            //3. feladat
            Console.WriteLine("#");
            (int utolsoMegallo, int lepes) KUKU = (0, 0);
            (int utolsoMegallo, int lepes) PIPI = (x.Count, 0);
            int ido = 0;
            while (true)
            {
                int tempIdo = x[KUKU.utolsoMegallo].varKUKU + x[KUKU.utolsoMegallo].jobbraIDO;
                var tempKUKU = (KUKU.utolsoMegallo + 1, 0);
                var tempPIPI = (PIPI.utolsoMegallo, PIPI.lepes);
                while (tempIdo == 0)
                {
                    if (x[PIPI.utolsoMegallo].varPIPI != 0)
                        --x[PIPI.utolsoMegallo].varPIPI;
                    else
                    {
                        if (PIPI.lepes != x[PIPI.utolsoMegallo].balraIDO)
                            tempPIPI = (tempPIPI.utolsoMegallo, tempPIPI.lepes + 1);
                        else
                            tempPIPI = (tempPIPI.utolsoMegallo - 1, 0);
                    }
                    --tempIdo;
                }
                if (tempKUKU.Item1 > tempPIPI.utolsoMegallo || (tempKUKU.Item1 == tempPIPI.utolsoMegallo && tempPIPI.lepes > 0)) //Túlment
                {

                }
                else if (tempKUKU.Item1 < tempPIPI.utolsoMegallo || (tempKUKU.Item1 == tempPIPI.utolsoMegallo && tempPIPI.lepes == 0))//mehet tovább mert egyszerre értek be
                {

                }

                //meg kell nézni, hogyha megvárjuk a várakozási időt KUKU val akkor elindul e
                //else if(tempKUKU.Item1 + 1 == tempPIPI.utolsoMegallo && tempKUKU.Item2 == tempPIPI.lepes)//pont egyszerre értek be a megá
                //ha továbbment mint az első, akkor az elsőnek várakoznia kell, ha pont ugyanott vannak vagy még nem ment túl az elsőn akkor mehet tovább

            }
        }
    }
}
