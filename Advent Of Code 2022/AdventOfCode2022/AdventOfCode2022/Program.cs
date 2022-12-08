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

        public static void Otodik()
        {
            List<Stack<char>> cargo = new List<Stack<char>>();
            //string[] s = File.ReadAllLines("otodik.txt");
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

        public static void Hatodik()
        {
            string s = File.ReadAllLines("hatodik.txt").First();
            Queue<char> activeFour = new Queue<char>();
            
            for (int i = 0; i < 3; i++)
                activeFour.Enqueue(s[i]);

            int index = 4;

            for (int i = 3; i < s.Length; i++)
            {
                index = i + 1;
                activeFour.Enqueue(s[i]);
                if (activeFour.Count(kk => activeFour.Count(zz => zz == kk) == 1) == 4)
                    break;

                activeFour.Dequeue();
            }

            Console.WriteLine("First start-of-packet marker pos: " + index);

            //2. rész
            Queue<char> activeFourteen = new Queue<char>();

            for (int i = 0; i < 13; i++)
                activeFourteen.Enqueue(s[i]);

            index = 14;

            for (int i = 13; i < s.Length; i++)
            {
                index = i + 1;
                activeFourteen.Enqueue(s[i]);
                if (activeFourteen.Count(kk => activeFourteen.Count(zz => zz == kk) == 1) == 14)
                    break;

                activeFourteen.Dequeue();
            }

            Console.WriteLine("First start-of-message marker pos: " + index);
        }

        public static void Hetedik()
        {
            
            List<string> s = File.ReadAllLines("hetedik.txt").ToList();
            //List<(string dir, int size)> x = new List<(string dir, int size)>();
            Dictionary<string, int> x = new Dictionary<string, int>();
            x.Add("/", 0);
            Stack<string> filesThatImIn = new Stack<string>();
            //több elkérési útvonal lehet / egy szinten lehet több ugyanolyan is

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');
                if (temp[0] == "$") //operation
                {
                    if (temp[1] == "ls")
                        continue;

                    if (temp[2] == "/")
                    {
                        filesThatImIn.Clear();
                        filesThatImIn.Push("/");
                    }
                    else if (temp[2] == "..")
                        filesThatImIn.Pop();
                    else
                        filesThatImIn.Push(concatStack(filesThatImIn) + temp[2]);
                }
                else if (temp[0] == "dir")
                {
                    if (!x.ContainsKey(concatStack(filesThatImIn) + temp[1]))
                        x.Add(concatStack(filesThatImIn) + temp[1], 0);
                }
                else
                    foreach (var i in filesThatImIn)
                        x[i] += int.Parse(temp[0]);
            }
            Console.WriteLine("Total size of at most 100 000' folders: " + x.Where(kk => kk.Value <= 100000).Sum(kk => kk.Value));

            //2. rész
            Console.WriteLine("Folder size: " + x.Where(kk => kk.Value >= 30000000 - (70000000 - x["/"])).OrderBy(kk => kk.Value).First().Value);
        }

        public static void Nyolcadik()
        {
            //In ASCII the numbers start at 48 ( == '0')
            List<List<int>> x = File.ReadAllLines("nyolcadik.txt").Select(kk => kk.Select(zz => zz - 48).ToList()).ToList();

            int dbNotVisible = 0;
            for (int i = 1; i < x.Count - 1; i++)
                for (int j = 1; j < x[i].Count - 1; j++)
                {
                    //List<int> onLeft = x[i].Take(j).ToList();
                    //List<int> onRight = x[i].TakeLast(x[i].Count - j).ToList();
                    //List<int> onTop = x.Select(kk => kk[j]).Take(i).ToList();
                    //List<int> onBottom = x.Select(kk => kk[j]).TakeLast(x.Count - i).ToList();

                    //onLeft
                    if (x[i].Take(j).All(kk => kk < x[i][j]))
                        continue;

                    //onRight
                    if (x[i].TakeLast(x[i].Count - j - 1).All(kk => kk < x[i][j]))
                        continue;

                    //onTop
                    if (x.Select(kk => kk[j]).Take(i).All(kk => kk < x[i][j]))
                        continue;

                    //onBottom
                    if (x.Select(kk => kk[j]).TakeLast(x.Count - i - 1).All(kk => kk < x[i][j]))
                        continue;

                    ++dbNotVisible;
                }

            Console.WriteLine("Visible trees: " + (x.Count * x[0].Count - dbNotVisible));

            //2. rész

            int maxViewDistance = 0;
            for (int i = 0; i < x.Count; i++)
                for (int j = 1; j < x[i].Count - 1; j++)
                {
                    //List<int> onLeft = x[i].Take(j).ToList();
                    //List<int> onRight = x[i].TakeLast(x[i].Count - j).ToList();
                    //List<int> onTop = x.Select(kk => kk[j]).Take(i).ToList();
                    //List<int> onBottom = x.Select(kk => kk[j]).TakeLast(x.Count - i).ToList();

                    int tempMax = 1;
                    //onLeft
                    //tempMax *= x[i].Take(j).Reverse().TakeWhile(kk => kk < x[i][j]).Count() + 1;
                    int temp = 0;
                    foreach (var item in x[i].Take(j).Reverse())
                    {
                        ++temp;
                        if (item >= x[i][j])
                            break;
                    }
                    tempMax *= temp;

                    //onRight
                    //tempMax *= x[i].TakeLast(x[i].Count - j - 1).TakeWhile(kk => kk < x[i][j]).Count() + 1;
                    temp = 0;
                    foreach (var item in x[i].TakeLast(x[i].Count - j - 1))
                    {
                        ++temp;
                        if (item >= x[i][j])
                            break;
                    }
                    tempMax *= temp;

                    //onTop
                    //tempMax *= x.Select(kk => kk[j]).Take(i).Reverse().TakeWhile(kk => kk < x[i][j]).Count() + 1;
                    temp = 0;
                    foreach (var item in x.Select(kk => kk[j]).Take(i).Reverse())
                    {
                        ++temp;
                        if (item >= x[i][j])
                            break;
                    }
                    tempMax *= temp;

                    //onBottom
                    //tempMax *= x.Select(kk => kk[j]).TakeLast(x.Count - i - 1).TakeWhile(kk => kk < x[i][j]).Count() + 1;
                    temp = 0;
                    foreach (var item in x.Select(kk => kk[j]).TakeLast(x.Count - i - 1))
                    {
                        ++temp;
                        if (item >= x[i][j])
                            break;
                    }
                    tempMax *= temp;

                    if (tempMax > maxViewDistance)
                        maxViewDistance = tempMax;
                }

            Console.WriteLine("The highest scenic score possible: " + maxViewDistance);

        }

        private static string concatStack(Stack<string> stack)
        {
            string solution = "";
            foreach (var item in stack.Where(kk => kk != "/").Select(kk => kk.Substring(1)))
                solution += @"\" + item;

            return solution;
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
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            Console.WriteLine("Melyik feladat: ");
            int n = int.Parse(Console.ReadLine());

            stopwatch.Start();

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
                case 6:
                    Hatodik();
                    break;
                case 7:
                    Hetedik();
                    break;
                case 8:
                    Nyolcadik();
                    break;
                default:
                    Console.WriteLine("Nincs ilyen feladat");
                    break;
            }

            Console.WriteLine("Eltelt idő: " + stopwatch.Elapsed);

        }
    }
}
