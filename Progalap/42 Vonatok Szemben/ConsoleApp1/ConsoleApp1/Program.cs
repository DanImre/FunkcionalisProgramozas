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

            x.Insert(0, new megallo(K_ind, menetidoMegallokKozott[0], -1, -1));
            x.Insert(x.Count, new megallo(menetidoMegallokKozott[menetidoMegallokKozott.Count-1],P_ind, -1, -1));
            //3. feladat
            /*
            int aktKUKU = K_ind;
            int aktPIPI = P_ind;
            (int where, int howMuch) varakozas = (0, 0);*/

            (int megallo, int elteltido) aktKUKU = (0,0);
            (int megallo, int elteltido) aktPIPI = (0,0);
            int time = 0;
            while (true)
            {
                //KUKU:
                if(x[aktKUKU.megallo].idoFromLeft == 0)
                {
                    if(x[aktKUKU.megallo].megalleKukutyin.HasValue)
                }
            }
            /*
            for (int i = 0; i < x.Count-1; i++)
            {
                if(Math.Abs(i - (x.Count-1 -i)) == 1 //(i,aktKUKU,aktPIPI))
                {
                    //kevesebb e az odaút, mint a várakozási idő
                    if (x[i].idoFromRight > x[x.Count - 1 - i].idoFromLeft) //aktPIPI indul előbb, neki van elsőbbség
                    {
                        varakozas = ((x.Count - 1 - i), );
                    }
                    else //aktKUKu elsőbbség
                    {

                    }
                }
                aktKUKU += x[i].idoFromLeft;
                if (x[i].megalleKukutyin.HasValue)
                    aktKUKU += x[i].megalleKukutyin.Value;

                aktPIPI += x[x.Count - i - 1].idoFromRight;
                if (x[x.Count - i - 1].megallePiripócs.HasValue)
                    aktPIPI += x[x.Count - i - 1].megallePiripócs.Value;

            }*/
        }
    }
}
