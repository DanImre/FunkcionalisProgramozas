using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2022
{
    class Program
    {
        public static void Elso()
        {
            List<string> s = File.ReadAllLines("elso.txt").ToList();
            List<int> elfs = new List<int>();
            while (s.Count > 0)
            {
                int temp = 0;
                while (s.Count > 0 && s[0] != "")
                {
                    temp += int.Parse(s[0]);
                    s.RemoveAt(0);
                }
                elfs.Add(temp);
                if (s.Count != 0)
                    s.RemoveAt(0);
            }
            Console.WriteLine("Top calories: " + elfs.Max());

            //2. rész:
            elfs.Sort();
            Console.WriteLine("Sum of the top 3 calories: " + elfs[elfs.Count - 1] + elfs[elfs.Count - 2] + elfs[elfs.Count - 3]);
        }

        public static void Masodik()
        {
            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("A", 1);
            values.Add("B", 2);
            values.Add("C", 3);
            values.Add("X", 1);
            values.Add("Y", 2);
            values.Add("Z", 3);

            int sum = 0;

            string[] s = File.ReadAllLines("masodik.txt");
            foreach (var item in s)
            {
                string[] temp = item.Split(' ');
                int enemyValue = values[temp[0]];
                int myValue = values[temp[1]];

                sum += myValue;

                if (myValue == enemyValue) //draw
                    sum += 3;
                else if (myValue - enemyValue == 1 || myValue - enemyValue == -2) //win
                    sum += 6;
            }

            Console.WriteLine("Total score: " + sum);

            //2. rész
            sum = 0;
            foreach (var item in s)
            {
                string[] temp = item.Split(' ');
                int enemyValue = values[temp[0]];
                int myValue = values[temp[1]];

                if (myValue == 1) //Need to lose
                    sum += (enemyValue - 1 == 0) ? 3 : enemyValue - 1;
                else if (myValue == 2) //draw
                    sum += 3 + enemyValue;
                else //win
                    sum += 6 + (enemyValue + 1 == 4 ? 1 : enemyValue + 1);
            }
            Console.WriteLine("Total score with the top secret strategy: " + sum);
        }

        public static void Harmadik()
        {
            string[] s = File.ReadAllLines("harmadik.txt");

            int sum = 0;

            foreach (var item in s)
            {
                string firstCompartment = item.Substring(0, item.Length / 2);
                string secondCompartment = item.Substring(item.Length / 2);

                //We now there is exactly 1 duplicate
                //converting the char to it's ASCII value
                int duplicate = firstCompartment.Where(kk => secondCompartment.Contains(kk)).First();

                sum += duplicate < 91 ? duplicate - 38 : duplicate - 96;
            }

            Console.WriteLine("Sum of the priorities: " + sum);

            //2. rész
            int index = 0;
            sum = 0;

            while (index < s.Length)
            {
                int badge = s[index].Where(kk => s[index + 1].Contains(kk) && s[index + 2].Contains(kk)).First();
                sum += badge < 91 ? badge - 38 : badge - 96;

                index += 3;
            }

            Console.WriteLine("Sum of the priorities of badges: " + sum);
        }
        
        public static void Negyedik()
        {
            string[] s = File.ReadAllLines("negyedik.txt");
            int db = 0;
            foreach (var item in s)
            {
                string[] temp = item.Split(',');
                int[] elso = temp[0].Split('-').Select(kk => int.Parse(kk)).ToArray();
                int[] masodik = temp[1].Split('-').Select(kk => int.Parse(kk)).ToArray();
                if ((elso[0] >= masodik[0] && elso[1] <= masodik[1]) || (elso[0] <= masodik[0] && elso[1] >= masodik[1]))
                    db++;
            }
            Console.WriteLine("Containing pairs: " + db);

            //2. rész
            db = 0;
            List<(int, int)> ranges = new List<(int, int)>();
            foreach (var item in s)
            {
                string[] temp = item.Split(',');
                int[] elso = temp[0].Split('-').Select(kk => int.Parse(kk)).ToArray();
                int[] masodik = temp[1].Split('-').Select(kk => int.Parse(kk)).ToArray();
                if ((elso[0] <= masodik[0] && elso[1] >= masodik[0]) || (masodik[0] <= elso[0] && masodik[1] >= elso[0]))
                    db++;
            }
            Console.WriteLine("Overlaping pairs: " + db);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Melyik feladat: ");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    Elso();
                    break;
                case 2:
                    Masodik();
                    break;
                case 3:
                    Harmadik();
                    break;
                case 4:
                    Negyedik();
                    break;
                default:
                    Console.WriteLine("Nincs ilyen feladat");
                    break;
            }
        }
    }
}
