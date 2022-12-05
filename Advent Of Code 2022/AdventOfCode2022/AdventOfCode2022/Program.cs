using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2022
{
    class Program
    {

        public class valami
        {
            public int age = 22;
            public int height = 180;

        }
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

        public static void Otodik()
        {
            List<Stack<char>> cargo = new List<Stack<char>>();
            string[] s = File.ReadAllLines("otodik.txt");

            //Searches for the line ' 1   2   3   4   5   6   7   8   9 ' and gets the last integer
            int howManyStacks = int.Parse(s.Where(kk => kk[1] == '1').First().Trim().Last().ToString());
            for (int i = 0; i < howManyStacks; i++)
                cargo.Add(new Stack<char>());

            foreach (var i in s)
            {
                if (i[1] == '1')
                    break;

                //removing first char
                string item = i.Remove(0, 1);
                for (int j = 0; j < item.Length; j++)
                {
                    //getting eery forth char (excluding '[', ']' and extra spaces
                    if (j % 4 != 0 || item[j] == ' ')
                        continue;

                    cargo[j/4].Push(item[j]);
                }
            }

            //reversing the order
            for (int i = 0; i < howManyStacks; i++)
                cargo[i] = new Stack<char>(cargo[i]); //Works cuz it uses '.Push' in the constructor

            //selecting the steps
            s = s.Where(kk => kk.IndexOf('m') == 0).ToArray();

            foreach (var item in s)
            {
                //[0] -> move | [1] -> from | [2] -> where
                int[] temp = item.Split(' ').Where(kk => !kk.Contains('o')).Select(kk => int.Parse(kk)).ToArray();

                //moving crates
                for (int i = 0; i < temp[0]; i++)
                    cargo[temp[2] - 1].Push(cargo[temp[1] - 1].Pop());
            }

            Console.Write("CrateMover 9000 toppings: ");
            foreach (var item in cargo)
                    Console.Write(item.Peek());

            Console.WriteLine();

            //2. rész

            cargo = new List<Stack<char>>();
            s = File.ReadAllLines("otodik.txt");

            for (int i = 0; i < howManyStacks; i++)
                cargo.Add(new Stack<char>());

            foreach (var i in s)
            {
                if (i[1] == '1')
                    break;

                //removing first char
                string item = i.Remove(0, 1);
                for (int j = 0; j < item.Length; j++)
                {
                    if (j % 4 != 0 || item[j] == ' ')
                        continue;

                    cargo[j / 4].Push(item[j]);
                }
            }

            for (int i = 0; i < howManyStacks; i++)
                cargo[i] = new Stack<char>(cargo[i]);

            s = s.Where(kk => kk.IndexOf('m') == 0).ToArray();

            foreach (var item in s)
            {
                int[] temp = item.Split(' ').Where(kk => !kk.Contains('o')).Select(kk => int.Parse(kk)).ToArray();

                Stack<char> tempStack = new Stack<char>();

                for (int i = 0; i < temp[0]; i++)
                    tempStack.Push(cargo[temp[1] - 1].Pop());

                for (int i = 0; i < temp[0]; i++)
                    cargo[temp[2] - 1].Push(tempStack.Pop());
            }

            Console.Write("CrateMover 9001 toppings: ");
            foreach (var item in cargo)
                Console.Write(item.Peek());
            Console.WriteLine();
        }

        //for testing
        private static void stackRepresentation(List<Stack<char>> y)
        {
            List<Stack<char>> x = new List<Stack<char>>();
            foreach (var item in y)
                x.Add(new Stack<char>(item.Reverse()));

            while (x.Count(kk => kk.Count != 0) != 0)
            {
                foreach (var item in x)
                {
                    if (item.Count == 0)
                        Console.Write("  ");
                    else
                        Console.Write(item.Pop() + " ");
                }
                Console.WriteLine();
            }
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
                case 5:
                    Otodik();
                    break;
                default:
                    Console.WriteLine("Nincs ilyen feladat");
                    break;
            }
        }
    }
}
