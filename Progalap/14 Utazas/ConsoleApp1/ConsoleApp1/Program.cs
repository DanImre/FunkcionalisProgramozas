using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int uthossz = int.Parse(s[0]);
            int tankolasDB = int.Parse(s[1]);
            int kezdoBenzin = int.Parse(s[2]);
            int fogyasztas = int.Parse(s[3]);

            List<(int hely, int mennyiseg)> x = new List<(int hely, int mennyiseg)>();
            for (int i = 0; i < tankolasDB; i++)
            {
                s = Console.ReadLine().Split(' ');
                x.Add((int.Parse(s[0]), int.Parse(s[1])));
            }

            //1. feladat
            Console.WriteLine(kezdoBenzin - (uthossz / 100 * fogyasztas) + x.Sum(kk => kk.mennyiseg));

            //2. feladat
            int aktbenzin = kezdoBenzin - x[0].hely / 100 * fogyasztas + x[0].mennyiseg;
            int max = aktbenzin > kezdoBenzin ? aktbenzin : kezdoBenzin;
            for (int i = 1; i < x.Count; i++)
            {
                aktbenzin += x[i].mennyiseg - (x[i].hely - x[i - 1].hely) / 100 * fogyasztas;
                if (aktbenzin > max)
                    max = aktbenzin;
            }
            Console.WriteLine(max);

            //3. feladat
            int? index = null;
            aktbenzin = kezdoBenzin;
            int megtett = 0;
            for (int i = 0; i < x.Count; i++)
            {
                aktbenzin += x[i].mennyiseg - (x[i].hely - megtett) / 100 * fogyasztas;
                megtett = x[i].hely;
                if ((uthossz - megtett) / 100 * fogyasztas <= aktbenzin)
                {
                    index = i;
                    break;
                }
            }
            //lehet számít a 0. tankolás ?
            if(!index.HasValue)
                Console.WriteLine(0);
            else
                Console.WriteLine(index.Value + 1);

            //4. feladat
            List<int> indexes = new List<int>();
            aktbenzin = kezdoBenzin;
            int elozobenzin = kezdoBenzin;
            megtett = 0;
            for (int i = 0; i < x.Count; i++)
            {
                aktbenzin += x[i].mennyiseg - (x[i].hely - megtett) / 100 * fogyasztas;
                megtett = x[i].hely;

                if (aktbenzin > elozobenzin)
                    indexes.Add(i);

                elozobenzin = aktbenzin;
            }

            Console.Write(indexes.Count + " ");
            foreach (var item in indexes)
                Console.Write((item + 1) + " ");
            Console.WriteLine();

            //5. feladat
            aktbenzin = kezdoBenzin - x[0].hely/100*fogyasztas + x[0].mennyiseg;
            megtett = x[0].hely;
            int? start = null;
            int end = 0;
            int maxtav = 0;
            int? tempStart = null;
            int counter = 0;
            for (int index2 = 1; index2 < x.Count; index2++)
            {
                aktbenzin -= (x[index2].hely - megtett) / 100 * fogyasztas;
                if (aktbenzin >= kezdoBenzin)
                {
                    if (!tempStart.HasValue)
                        tempStart = index2 - 1;

                    ++counter;
                }
                else
                {
                    tempStart = null;
                    counter = 0;
                }

                aktbenzin += x[index2].mennyiseg;
                megtett = x[index2].hely;

                if (tempStart.HasValue)
                {
                    if (!start.HasValue)
                    {
                        start = tempStart.Value;
                        end = tempStart.Value + counter;
                    }
                    else if (maxtav <= x[start.Value + counter].hely - x[start.Value].hely)
                    {
                        start = tempStart.Value;
                        end = tempStart.Value + counter;
                    }
                }
            }
            if (start.HasValue)
                Console.WriteLine(x[end].hely - x[start.Value].hely);
            else
                Console.WriteLine(0);
        }
    }
}
