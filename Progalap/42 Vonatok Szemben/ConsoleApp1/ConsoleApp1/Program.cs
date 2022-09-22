using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        class megallo
        {
            public int? megallePiripócs { get; set; }
            public int? megalleKukutyin { get; set; }

            public int idoFromLeft { get; set; }
            public int idoFromRight { get; set; }

            public megallo(int _idoFromLeft, int _idoFromRight, int? _megallePiripócs = null, int? _megalleKukutyin = null)
            {
                idoFromLeft = _idoFromLeft;
                idoFromRight = _idoFromRight;
                megallePiripócs = _megallePiripócs;
                megalleKukutyin = _megalleKukutyin;
            }
        }
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int K_ind = int.Parse(s[0]);
            int P_ind = int.Parse(s[1]);
            int n = int.Parse(s[2]);

            s = Console.ReadLine().Split(' ');//idők
            List<int> menetidoMegallokKozott = new List<int>();
            foreach (var item in s)
                menetidoMegallokKozott.Add(int.Parse(item));

            string[] varakozasokKUKUSTRING = Console.ReadLine().Split(' ');//varakozás Kukutyinból
            List<int> varakozasokKUKU = new List<int>();
            foreach (var item in varakozasokKUKUSTRING)
                varakozasokKUKU.Add(int.Parse(item));
            string[] varakozasokPIRISTRING = Console.ReadLine().Split(' ');//varakozás Piripócs
            List<int> varakozasokPIRI = new List<int>();
            foreach (var item in varakozasokPIRISTRING)
                varakozasokPIRI.Add(int.Parse(item));

            int db = 0;
            for (int i = 0; i < n; i++)
                if (varakozasokKUKU[i] == 0 && varakozasokPIRI[n - i - 1] == 0)
                    ++db;

            //1. feladat
            Console.WriteLine("#");
            Console.WriteLine(db);

            //2. feladat
            Console.WriteLine("#");
            int KUKUido = K_ind;
            int PIPIido = P_ind;
            foreach (var item in menetidoMegallokKozott)
            {
                KUKUido += item;
                PIPIido += item;
            }

            foreach (var item in varakozasokKUKU)
                KUKUido += item;

            foreach (var item in varakozasokPIRI)
                PIPIido += item;

            Console.WriteLine(KUKUido + " " + PIPIido);

            //3. feladat
            Console.WriteLine("#");
            List<megallo> x = new List<megallo>();
            for (int i = 0; i < n; i++)
                x.Add(new megallo(0, 0));

            for (int i = 0; i < menetidoMegallokKozott.Count-1; i++)
            {
                x[i].idoFromLeft = menetidoMegallokKozott[i];
                x[i].idoFromRight = menetidoMegallokKozott[i + 1];
            }

            for (int i = 0; i < n; i++)
            {
                if (varakozasokKUKU[i] > 0)
                    x[i].megalleKukutyin = varakozasokKUKU[i];
                if (varakozasokPIRI[i] > 0)
                    x[i].megallePiripócs = varakozasokPIRI[i];
            }

            x.Insert(0, new megallo(0, menetidoMegallokKozott[0], -1, K_ind));
            x.Insert(x.Count, new megallo(menetidoMegallokKozott[menetidoMegallokKozott.Count-1],0, P_ind, -1));
            
            //3. feladat
            int aktKUKUmeg = 0;
            int aktPIPImeg = x.Count - 1;
            int time = 0;
            int waitTillKukuGoes = 0;
            int waitTillPipiGoes = x.Count-1;

            int TimeTakenKuku = 0;
            int TimeTakenPipi = 0;
            while ((TimeTakenKuku == 0 || TimeTakenPipi == 0) && time < 1000)
            {
                ++time;

                //KUKU:
                if (waitTillPipiGoes == aktPIPImeg)
                {
                    if (x[aktKUKUmeg].idoFromRight == 0)
                    {
                        if (!x[aktKUKUmeg].megalleKukutyin.HasValue || x[aktKUKUmeg].megalleKukutyin.Value == 0)
                        {


                            ++aktKUKUmeg;
                            waitTillKukuGoes = aktKUKUmeg;
                        }
                        else
                            x[aktKUKUmeg].megalleKukutyin -= 1;
                    }
                    else
                        x[aktKUKUmeg].idoFromRight -= 1;
                }
                else if (x[aktKUKUmeg].megalleKukutyin.HasValue && x[aktKUKUmeg].megalleKukutyin.Value != 0)
                    --x[aktKUKUmeg].megalleKukutyin;

                //PIPI:
                if (waitTillKukuGoes == aktKUKUmeg)
                {
                    if (x[aktPIPImeg].idoFromLeft == 0)
                    {
                        if (!x[aktPIPImeg].megallePiripócs.HasValue || x[aktPIPImeg].megallePiripócs.Value == 0)
                        {
                            --aktPIPImeg;
                            waitTillPipiGoes = aktPIPImeg;
                        }
                        else
                            x[aktPIPImeg].megallePiripócs -= 1;
                    }
                    else
                        x[aktPIPImeg].idoFromLeft -= 1;
                }
                else if (x[aktPIPImeg].megallePiripócs.HasValue && x[aktPIPImeg].megallePiripócs.Value != 0)
                    --x[aktPIPImeg].megallePiripócs;

                //ha ugyanazon az úton vannak (pl 0 és 1) megnézzük hogy karambol lenne-e, ha igen a később indulót várakoztatjuk és számoljuk
                if(waitTillKukuGoes == aktKUKUmeg && waitTillPipiGoes == aktPIPImeg)
                if (aktPIPImeg - aktKUKUmeg == 1 && ((!x[aktKUKUmeg].megalleKukutyin.HasValue || x[aktKUKUmeg].megalleKukutyin.Value == 0) || (!x[aktPIPImeg].megalleKukutyin.HasValue || x[aktPIPImeg].megalleKukutyin.Value == 0)))
                {
                    int tempKUKU = x[aktKUKUmeg].megalleKukutyin.HasValue ? x[aktKUKUmeg].megalleKukutyin.Value : 0;
                    int tempPIPI = x[aktPIPImeg].megallePiripócs.HasValue ? x[aktPIPImeg].megallePiripócs.Value : 0;

                    if (tempKUKU != 0 && tempPIPI == 0)
                    {
                        --waitTillPipiGoes;
                    }
                    else
                    {
                        ++waitTillKukuGoes;
                    }
                }

                if (aktKUKUmeg == x.Count - 1)
                    TimeTakenKuku = time;
                if (aktPIPImeg == 0)
                    TimeTakenPipi = time;

                Console.WriteLine("time : " + time + " KUKU waiting ? " + (waitTillKukuGoes != aktKUKUmeg ? "Igen" : "Nem") + " PIPI waiting ? " + (waitTillPipiGoes != aktPIPImeg ? "Igen" : "Nem"));
            }

            Console.WriteLine(TimeTakenKuku + " " + TimeTakenPipi);
        }
    }
}
