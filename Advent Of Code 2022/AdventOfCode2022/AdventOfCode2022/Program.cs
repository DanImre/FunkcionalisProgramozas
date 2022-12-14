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

        public static void Kilencedik()
        {
            string[] s = File.ReadAllLines("kilencedik.txt");
            (int x, int y) head = (0, 0);
            (int x, int y) tail = (0, 0);

            List<(int x, int y)> visitedCords = new List<(int x, int y)>() { (0,0) };

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');

                (int x, int y) increment = (0, 0);

                switch (temp[0])
                {
                    case "U":
                        increment = (0, 1);
                        break;
                    case "D":
                        increment = (0, -1);
                        break;
                    case "R":
                        increment = (1, 0);
                        break;
                    default:
                        increment = (-1, 0);
                        break;
                }

                for (int i = 0; i < int.Parse(temp[1]); i++)
                {
                    if(Math.Abs(head.x + increment.x - tail.x) <= 1 && Math.Abs(head.y + increment.y - tail.y) <= 1)
                    {
                        head = (head.x + increment.x, head.y + increment.y);
                        continue;
                    }

                    tail = head;
                    if (!visitedCords.Contains(tail))
                        visitedCords.Add(tail);
                    head = (head.x + increment.x, head.y + increment.y);
                }
            }

            Console.WriteLine("The tail visited: " + visitedCords.Count + " spots.");
            
            //2. rész

            //1. -> Head | 2.-10. -> Tails
            List<(int x, int y)> headsAndTails = new List<(int x, int y)>();
            for (int i = 0; i < 10; i++)
                headsAndTails.Add((0, 0));

            visitedCords.Clear();
            visitedCords.Add((0, 0));

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');

                (int x, int y) increment = (0, 0);

                switch (temp[0])
                {
                    case "U":
                        increment = (0, 1);
                        break;
                    case "D":
                        increment = (0, -1);
                        break;
                    case "R":
                        increment = (1, 0);
                        break;
                    default:
                        increment = (-1, 0);
                        break;
                }

                for (int i = 0; i < int.Parse(temp[1]); i++)
                {
                    //head moves
                    headsAndTails[0] = (headsAndTails[0].x + increment.x, headsAndTails[0].y + increment.y);

                    //diagonális lépésnél a lépéseket kell követni, egyébként csak a helyére kell menni
                    
                    for (int j = 1; j < headsAndTails.Count(); j++)
                    {
                        (int x, int y) tavolsag = (headsAndTails[j - 1].x - headsAndTails[j].x, headsAndTails[j - 1].y - headsAndTails[j].y);

                        //doesnt need to move
                        if (Math.Abs(tavolsag.x) <= 1 && Math.Abs(tavolsag.y) <= 1)
                            break;

                        //jobbrafel
                        if (tavolsag.x > 0 && tavolsag.y > 0)
                            headsAndTails[j] = (headsAndTails[j].x + 1, headsAndTails[j].y + 1);
                        //balrafel
                        else if(tavolsag.x < 0 && tavolsag.y > 0)
                            headsAndTails[j] = (headsAndTails[j].x - 1, headsAndTails[j].y + 1);
                        //jobbrale
                        else if(tavolsag.x > 0 && tavolsag.y < 0)
                            headsAndTails[j] = (headsAndTails[j].x + 1, headsAndTails[j].y - 1);
                        //balrale
                        else if(tavolsag.x < 0 && tavolsag.y < 0)
                            headsAndTails[j] = (headsAndTails[j].x - 1, headsAndTails[j].y - 1);
                        //jobbra
                        else if (tavolsag.x > 0)
                            headsAndTails[j] = (headsAndTails[j].x + 1, headsAndTails[j].y);
                        //balra
                        else if (tavolsag.x < 0)
                            headsAndTails[j] = (headsAndTails[j].x - 1, headsAndTails[j].y);
                        //fel
                        else if (tavolsag.y > 0)
                            headsAndTails[j] = (headsAndTails[j].x, headsAndTails[j].y + 1);
                        //le
                        else
                            headsAndTails[j] = (headsAndTails[j].x, headsAndTails[j].y - 1);

                        if (j == 9 && !visitedCords.Contains(headsAndTails[j]))
                            visitedCords.Add(headsAndTails[j]);
                    }
                }

                //ki
                /*
                kiirasKilencedik(headsAndTails);
                */
            }

            Console.WriteLine("The 9th tail visited: " + visitedCords.Count + " spots.");
        }

        public static void Tizedik()
        {
            string[] s = File.ReadAllLines("tizedik.txt");
            int sum = 0;
            int actValue = 1;
            int cycle = 0;

            foreach (var item in s)
            {
                if (item[0] == 'n')
                {
                    ++cycle;
                    if (cycle % 40 == 20)
                        sum += cycle * actValue;
                    continue;
                }


                for (int i = 0; i < 2; i++)
                {
                    ++cycle;
                    if (cycle % 40 == 20)
                        sum += cycle * actValue;
                }
                actValue += int.Parse(item.Split(' ')[1]);

            }
            Console.WriteLine("Sum of the signal strengths: " + sum);

            //2. rész:

            string[,] pixels = new string[6, 40];

            actValue = 1;
            cycle = 0;

            foreach (var item in s)
            {
                //start of the cycle

                //If it's close enough
                pixels[(int)Math.Floor(cycle / 40.0), cycle % 40] = Math.Abs((cycle % 40) - actValue) <= 1 ? "#" : ".";

                ++cycle;

                if (item[0] == 'n') //noop only takes 1 cycle
                    continue;

                //second half of the cycle
                pixels[(int)Math.Floor(cycle / 40.0), cycle % 40] = Math.Abs((cycle % 40) - actValue) <= 1 ? "#" : ".";
                
                ++cycle;

                actValue += int.Parse(item.Split(' ')[1]);
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 40; j++)
                    Console.Write(pixels[i,j]);
                Console.WriteLine();
            }
        }

        class Monkey
        {
            public int inspectedItems { get; set; }
            public List<long> items { get; set; }
            public Func<long, long> operation { get; set; }
            public Func<long, bool> test { get; set; }

            public int trueThrow { get; set; }
            public int falseThrow { get; set; }



            public Monkey(int a, List<long> b)
            {
                inspectedItems = a;
                items = b;
            }

            public Monkey()
            {
                inspectedItems = 0;
                items = new List<long>();
            }

        }

        public static void Tizenegyedik()
        {
            //string[] s = File.ReadAllLines("tizenegyedikproba.txt");
            string[] s = File.ReadAllLines("tizenegyedik.txt");
            List<Monkey> x = new List<Monkey>();

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');

                if (temp[0] == "Monkey")
                {
                    x.Add(new Monkey());
                    continue;
                }
                string sor = item.Trim();

                if(sor.Contains("items"))
                {
                    temp = sor.Split(':').Last().Split(',').Select(kk => kk.Trim()).ToArray();

                    foreach (var i in temp)
                        x[x.Count - 1].items.Add(int.Parse(i));

                    continue;
                }

                if(sor.Contains("Operation"))
                {
                    sor = sor.Split('=').Last();

                    //+
                    if(sor.Contains('+'))
                    {
                        temp = sor.Split('+').Select(kk => kk.Trim()).ToArray();
                        if (temp[0] == "old" && temp[1] == "old")
                            x[x.Count - 1].operation = kk => kk + kk;
                        else
                            x[x.Count - 1].operation = kk => kk + int.Parse(temp[1]);
                        
                        continue;
                    }

                    //*
                    if (sor.Contains('*'))
                    {
                        temp = sor.Split('*').Select(kk => kk.Trim()).ToArray();
                        if (temp[0] == "old" && temp[1] == "old")
                            x[x.Count - 1].operation = kk => kk * kk;
                        else
                            x[x.Count - 1].operation = kk => kk * int.Parse(temp[1]);
                        
                        continue;
                    }
                }

                else if(sor.Contains("Test"))
                    x[x.Count - 1].test = (kk => kk % int.Parse(sor.Split(' ').Last()) == 0);
                else if(sor.Contains("true"))
                    x[x.Count - 1].trueThrow = int.Parse(sor.Split(' ').Last());
                else if(sor.Contains("false"))
                    x[x.Count - 1].falseThrow = int.Parse(sor.Split(' ').Last());

            }

            //1. rész

            for (int j = 0; j < 20; j++)
                for (int i = 0; i < x.Count; i++)
                {
                    foreach (var item in x[i].items)
                    {
                        int newItem = (int)Math.Floor(x[i].operation(item) / 3.0);
                        //long newItem = x[i].operation(item);
                        
                        if (x[i].test(newItem))
                            x[x[i].trueThrow].items.Add(newItem);
                        else
                            x[x[i].falseThrow].items.Add(newItem);
                        
                        ++x[i].inspectedItems;
                    }

                    x[i].items.Clear();
                }

            int solution = 1;
            foreach (var item in x.Select(kk => kk.inspectedItems).OrderByDescending(kk => kk).Take(2))
                solution *= item;

            Console.WriteLine("The level of monkey business after 20 rounds: " + solution);

            //2. rész

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');

                if (temp[0] == "Monkey")
                {
                    x.Add(new Monkey());
                    continue;
                }
                string sor = item.Trim();

                if (sor.Contains("items"))
                {
                    temp = sor.Split(':').Last().Split(',').Select(kk => kk.Trim()).ToArray();

                    foreach (var i in temp)
                        x[x.Count - 1].items.Add(int.Parse(i));

                    continue;
                }

                if (sor.Contains("Operation"))
                {
                    sor = sor.Split('=').Last();

                    //+
                    if (sor.Contains('+'))
                    {
                        temp = sor.Split('+').Select(kk => kk.Trim()).ToArray();
                        if (temp[0] == "old" && temp[1] == "old")
                            x[x.Count - 1].operation = kk => kk + kk;
                        else
                            x[x.Count - 1].operation = kk => kk + int.Parse(temp[1]);

                        continue;
                    }

                    //*
                    if (sor.Contains('*'))
                    {
                        temp = sor.Split('*').Select(kk => kk.Trim()).ToArray();
                        if (temp[0] == "old" && temp[1] == "old")
                            x[x.Count - 1].operation = kk => kk * kk;
                        else
                            x[x.Count - 1].operation = kk => kk * int.Parse(temp[1]);

                        continue;
                    }
                }

                else if (sor.Contains("Test"))
                    x[x.Count - 1].test = (kk => kk % int.Parse(sor.Split(' ').Last()) == 0);
                else if (sor.Contains("true"))
                    x[x.Count - 1].trueThrow = int.Parse(sor.Split(' ').Last());
                else if (sor.Contains("false"))
                    x[x.Count - 1].falseThrow = int.Parse(sor.Split(' ').Last());

            }

            long number = 1;
            foreach (var item in s)
            {
                if (item.Contains("Test"))
                    number *= long.Parse(item.Split(' ').Last());
            }

            for (int j = 0; j < 10000; j++)
                for (int i = 0; i < x.Count; i++)
                {
                    foreach (var item in x[i].items)
                    {
                        long newItem = x[i].operation(item);
                        newItem %= number;

                        if (x[i].test(newItem))
                            x[x[i].trueThrow].items.Add(newItem);
                        else
                            x[x[i].falseThrow].items.Add(newItem);

                        ++x[i].inspectedItems;
                    }

                    x[i].items.Clear();
                }

            long solutionTwo = 1;
            foreach (var item in x.Select(kk => kk.inspectedItems).OrderByDescending(kk => kk).Take(2))
                solutionTwo *= item;

            Console.WriteLine("The level of monkey business after 10000 rounds: " + solutionTwo);

        }

        public static void Tizenkettedik()
        {
            string[] s = File.ReadAllLines("tizenkettedik.txt");
            List<List<int>> x = new List<List<int>>();
            (int x, int y) kezdes = (0, 0);
            (int x, int y) veg = (0, 0);

            //beolvasás
            for (int i = 0; i < s.Length; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                    {
                        kezdes = (i, j);
                        temp.Add(0);
                        continue;
                    }

                    if (s[i][j] == 'E')
                    {
                        veg = (i, j);
                        temp.Add('z' - 97);
                        continue;
                    }

                    temp.Add(s[i][j] - 97);
                }
                x.Add(temp);
            }

            //setup
            List<List<int>> dist = new List<List<int>>();
            for (int i = 0; i < x.Count; i++)
            {
                List<int> tempdist = new List<int>();
                for (int j = 0; j < x[i].Count; j++)
                    tempdist.Add(int.MaxValue);

                dist.Add(tempdist);
            }

            dist[kezdes.x][kezdes.y] = 0;

            Queue<(int x, int y)> q = new Queue<(int x, int y)>();
            q.Enqueue(kezdes);

            //actual algorithm
            while (q.Count != 0)
            {
                var temp = q.Dequeue();

                if (temp.x == veg.x && temp.y == veg.y)
                    break;

                int mostaniErtek = x[temp.x][temp.y];
                int nextDist = dist[temp.x][temp.y] + 1;

                //jobbra
                if (temp.x < x.Count - 1 && x[temp.x + 1][temp.y] - mostaniErtek <= 1)
                {
                    if (nextDist < dist[temp.x + 1][temp.y])
                    {
                        q.Enqueue((temp.x + 1, temp.y));
                        dist[temp.x + 1][temp.y] = nextDist;
                    }
                }
                //balra
                if (temp.x > 0 && x[temp.x - 1][temp.y] - mostaniErtek <= 1)
                {
                    if (nextDist < dist[temp.x - 1][temp.y])
                    {
                        q.Enqueue((temp.x - 1, temp.y));
                        dist[temp.x - 1][temp.y] = nextDist;
                    }
                }
                //fel
                if (temp.y < x[0].Count - 1 && x[temp.x][temp.y + 1] - mostaniErtek <= 1)
                {
                    if (nextDist < dist[temp.x][temp.y + 1])
                    {
                        q.Enqueue((temp.x, temp.y + 1));
                        dist[temp.x][temp.y + 1] = nextDist;
                    }
                }
                //le
                if (temp.y > 0 && x[temp.x][temp.y - 1] - mostaniErtek <= 1)
                {
                    if (nextDist < dist[temp.x][temp.y - 1])
                    {
                        q.Enqueue((temp.x, temp.y - 1));
                        dist[temp.x][temp.y - 1] = nextDist;
                    }
                }
            }

            Console.WriteLine("Shortest path to E: " + dist[veg.x][veg.y]);

            //2. rész:

            List<int> solutions = new List<int>();

            for (int l = 0; l < x.Count; l++)
            {
                for (int k = 0; k < x[l].Count; k++)
                {
                    if (x[l][k] != 0)
                        continue;

                    //reseting everything
                    q = new Queue<(int x, int y)>();
                    q.Enqueue((l,k));

                    dist = new List<List<int>>();
                    for (int i = 0; i < x.Count; i++)
                    {
                        List<int> tempdist = new List<int>();
                        for (int j = 0; j < x[i].Count; j++)
                            tempdist.Add(int.MaxValue);

                        dist.Add(tempdist);
                    }

                    dist[l][k] = 0;

                    while (q.Count != 0)
                    {
                        var temp = q.Dequeue();

                        if (temp.x == veg.x && temp.y == veg.y)
                            break;

                        int mostaniErtek = x[temp.x][temp.y];
                        int nextDist = dist[temp.x][temp.y] + 1;

                        //jobbra
                        if (temp.x < x.Count - 1 && x[temp.x + 1][temp.y] - mostaniErtek <= 1)
                        {
                            if (nextDist < dist[temp.x + 1][temp.y])
                            {
                                q.Enqueue((temp.x + 1, temp.y));
                                dist[temp.x + 1][temp.y] = nextDist;

                            }
                        }
                        //balra
                        if (temp.x > 0 && x[temp.x - 1][temp.y] - mostaniErtek <= 1)
                        {
                            if (nextDist < dist[temp.x - 1][temp.y])
                            {
                                q.Enqueue((temp.x - 1, temp.y));
                                dist[temp.x - 1][temp.y] = nextDist;
                            }
                        }
                        //fel
                        if (temp.y < x[0].Count - 1 && x[temp.x][temp.y + 1] - mostaniErtek <= 1)
                        {
                            if (nextDist < dist[temp.x][temp.y + 1])
                            {
                                q.Enqueue((temp.x, temp.y + 1));
                                dist[temp.x][temp.y + 1] = nextDist;
                            }
                        }
                        //le
                        if (temp.y > 0 && x[temp.x][temp.y - 1] - mostaniErtek <= 1)
                        {
                            if (nextDist < dist[temp.x][temp.y - 1])
                            {
                                q.Enqueue((temp.x, temp.y - 1));
                                dist[temp.x][temp.y - 1] = nextDist;
                            }
                        }
                    }

                    //getting length
                    solutions.Add(dist[veg.x][veg.y]);
                }
            }

            Console.WriteLine("Shortest path from any 'a's: " + solutions.Min());

            //where it came from, so we can traverse it
            //List<List<(int x, int y)>> prev = new List<List<(int x, int y)>>();
            //we need to know if it had a 0 in its path
            //List<List<(int x, int y, int value)>> prev = new List<List<(int x, int y, int value)>>();
            //I dont even need this xD


        }

        class Item
        {
            public bool isList { get; set; }
            public int value { get; set; }
            public List<Item> list { get; set; }
            public Item parent { get; set; }

            public Item()
            {
                isList = true;
                list = new List<Item>();
            }

            public Item(int a)
            {
                isList = false;
                value = a;
            }

            public Item(Item a)
            {
                isList = true;
                list = new List<Item>() { a };
            }
        }

        public static void Tizenharmadik()
        {
            string[] s = File.ReadAllLines("tizenharmadik.txt").Where(kk => kk != "").ToArray();
            List<(Item bal, Item jobb)> x = new List<(Item bal, Item jobb)>();

            //beolvasás
            Item tempBal = new Item();
            Item tempJobb = new Item();

            for (int i = 0; i < s.Length; i++)
            {
                string sor = "";
                for (int uuu = 0; uuu < s[i].Length; uuu++)
                {
                    if (s[i][uuu] == ']')
                        sor += ',';

                    sor += s[i][uuu];

                    if (s[i][uuu] == '[')
                        sor += ',';
                }

                if(i % 2 == 0)
                {
                    foreach (var item in sor.Split(',').Where(kk => kk != ""))
                    {
                        if (item == "[")
                        {
                            Item temp = new Item();
                            tempBal.list.Add(temp);

                            temp.parent = tempBal;
                            tempBal = temp;
                        }
                        else if (item == "]")
                            tempBal = tempBal.parent;
                        else
                            tempBal.list.Add(new Item(int.Parse(item)));
                    }
                }
                else
                {
                    foreach (var item in sor.Split(',').Where(kk => kk != ""))
                    {
                        if (item == "[")
                        {
                            Item temp = new Item();
                            tempJobb.list.Add(temp);

                            temp.parent = tempJobb;
                            tempJobb = temp;
                        }
                        else if (item == "]")
                            tempJobb = tempJobb.parent;
                        else
                            tempJobb.list.Add(new Item(int.Parse(item)));
                    }


                    x.Add((tempBal.list[0], tempJobb.list[0]));

                    tempBal = new Item();
                    tempJobb = new Item();
                }

            }

            List<int> indices = new List<int>();
            for (int i = 0; i < x.Count; i++)
            {
                bool? result = compareTizenharom(x[i].bal, x[i].jobb);

                if (result.HasValue && result.Value == true)
                {
                    //Console.WriteLine("In the right order: " + (i + 1));
                    indices.Add(i + 1);
                }
            }

            Console.WriteLine("Sum of the indices of the 'right' pairs: " + indices.Sum());

            //2. rész

            List<Item> y = new List<Item>();
            foreach (var item in x)
            {
                y.Add(item.bal);
                y.Add(item.jobb);
            }
            y.Add(new Item(new Item(new Item(2))));
            y.Add(new Item(new Item(new Item(6))));

            for (int i = 0; i < y.Count; i++)
            {
                for (int j = i + 1; j < y.Count; j++)
                {
                    bool? result = compareTizenharom(y[i], y[j]);

                    //jó sorrend
                    if (!result.HasValue || result.Value == true)
                        continue;

                    //rossz sorrend => swap
                    Item temp = y[i];
                    y[i] = y[j];
                    y[j] = temp;
                }
            }

            int kettes = 0;
            int hatos = 0;

            for (int i = 0; i < y.Count; i++)
            {
                if (y[i].isList
                    && y[i].list.Count == 1
                    && y[i].list[0].isList
                    && y[i].list[0].list.Count == 1)
                {
                    if (y[i].list[0].list[0].value == 2)
                        kettes = i + 1;
                    else if (y[i].list[0].list[0].value == 6)
                        hatos = i + 1;
                }
            }

            Console.WriteLine("The decoder key for the distress signal: " + kettes * hatos);
            
            //kiiras
            /*
            Console.WriteLine(kettes + " " + hatos);
            itemKiiras(y[kettes - 1]);
            Console.WriteLine();
            itemKiiras(y[hatos - 1]);
            Console.WriteLine();

            //bizonyítás hogy jól van sortolva:
            for (int i = 0; i < y.Count - 1; i++)
            {
                itemKiiras(y[i]);
                Console.WriteLine((compareTizenharom(y[i], y[i + 1]).Value ? " | igen" : " | NEM" ));
            }
            */
        }

        private static bool? compareTizenharom(Item a, Item b)
        {
            //minkettő lista
            if (a.isList && b.isList)
            {
                int index = 0;
                while (index < a.list.Count && index < b.list.Count)
                {
                    bool? result = compareTizenharom(a.list[index], b.list[index]);

                    if (result.HasValue)
                        return result.Value;

                    ++index;
                }

                //egyszerre fogytak ki
                if (index == a.list.Count && index == b.list.Count)
                    return null;
                //'a' fogyott ki
                else if (index == a.list.Count)
                    return true;
                //'b' fogyott ki elobb
                else
                    return false;
            }
            //minkettő szam
            else if (!a.isList && !b.isList)
            {
                if (a.value == b.value)
                    return null;
                else if (a.value < b.value)
                    return true;
                else
                    return false;
            }
            //'a' lista, de 'b' nem
            else if (a.isList && !b.isList)
                return compareTizenharom(a, new Item(b)); //beletesszük egy listába
            //'b' lista, de 'a' nem
            else
                return compareTizenharom(new Item(a), b); //beletesszük egy listába
        }


        public static void Tizennegyedik()
        {

            //feltételezem, hogy mindig lehet homokot kreállni (nem lesz olyan, hogy be lesz zárva)
            //azt is, hogy akkor ér ki a homok, ha minden kőnél lejebb van (y kordinátája nagyobb mint akármelyik kőnek)
            string[] s = File.ReadAllLines("tizennegyedikarpi.txt");

            List<(int x, int y)> map = new List<(int x, int y)>();
            map.Add((-10, -10));

            (int x, int y) spawner = (500, 0);

            //beolvasás
            foreach (var item in s)
            {
                string[] temp = item.Split(" -> ");
                for (int i = 1; i < temp.Length; i++)
                {

                    int[] elsoCord = temp[i - 1].Split(',').Select(int.Parse).ToArray();
                    int[] masodikCord = temp[i].Split(',').Select(int.Parse).ToArray();
                    //ha x tengelyen volt mozgas:
                    if (elsoCord[0] - masodikCord[0] != 0)
                        for (int j = (elsoCord[0] < masodikCord[0] ? elsoCord[0] : masodikCord[0]); j <= (elsoCord[0] > masodikCord[0] ? elsoCord[0] : masodikCord[0]); j++)
                        {
                            if (map[map.Count - 1] != (j, elsoCord[1]))
                                map.Add((j, elsoCord[1]));
                        }
                    //ha y tengelyen
                    else
                        for (int j = (elsoCord[1] < masodikCord[1] ? elsoCord[1] : masodikCord[1]); j <= (elsoCord[1] > masodikCord[1] ? elsoCord[1] : masodikCord[1]); j++)
                        {
                            if (map[map.Count - 1] != (elsoCord[0], j))
                                map.Add((elsoCord[0], j));
                        }
                }
            }

            map.RemoveAt(0);

            #region sandmap kiiras
            /*
            for (int i = map.Min(kk => kk.y); i <= map.Max(kk => kk.y); i++)
            {
                for (int j = map.Min(kk => kk.x); j <= map.Max(kk => kk.x); j++)
                {
                    if (map.Contains((j, i)))
                        Console.Write("#");
                    else
                        Console.Write("_");
                }
                Console.WriteLine();
            }*/
            #endregion

            HashSet<(int x, int y)> x = new HashSet<(int x, int y)>();

            foreach (var item in map)
                x.Add(item);

            (int x, int y) aktSand = spawner;
            int amountOfRestedSand = 0;
            int finishline = map.Max(kk => kk.y) + 1;
            while (true)
            {
                //end condition
                if (aktSand.y == finishline)
                    break;
                //down
                else if (!x.Contains((aktSand.x, aktSand.y + 1)))
                    aktSand = (aktSand.x, aktSand.y + 1);
                //downLeft
                else if (!x.Contains((aktSand.x - 1, aktSand.y + 1)))
                    aktSand = (aktSand.x - 1, aktSand.y + 1);
                //downRight
                else if (!x.Contains((aktSand.x + 1, aktSand.y + 1)))
                    aktSand = (aktSand.x + 1, aktSand.y + 1);
                else
                {
                    x.Add(aktSand);
                    ++amountOfRestedSand;
                    aktSand = spawner;
                }
            }
            Console.WriteLine("Units of sand come to rest before sand starts flowing into the abyss below: " + amountOfRestedSand);

            //2. rész

            int floor = finishline + 1;

            while (true)
            {
                //end condition
                if (x.Contains(spawner))
                    break;


                //stop at floor
                if(aktSand.y == floor - 1)
                {
                    x.Add(aktSand);
                    ++amountOfRestedSand;
                    aktSand = spawner;
                }
                //le
                else if (!x.Contains((aktSand.x, aktSand.y + 1)))
                    aktSand = (aktSand.x, aktSand.y + 1);
                //balrale
                else if (!x.Contains((aktSand.x - 1, aktSand.y + 1)))
                    aktSand = (aktSand.x - 1, aktSand.y + 1);
                //jobbrale
                else if (!x.Contains((aktSand.x + 1, aktSand.y + 1)))
                    aktSand = (aktSand.x + 1, aktSand.y + 1);
                else
                {
                    x.Add(aktSand);
                    ++amountOfRestedSand;
                    aktSand = spawner;
                }
            }

            Console.WriteLine("Units of sand before blocking the spawner: " + amountOfRestedSand);
        }


        private static string concatStack(Stack<string> stack)
        {
            string solution = "";
            foreach (var item in stack.Where(kk => kk != "/").Select(kk => kk.Substring(1)))
                solution += @"\" + item;

            return solution;
        }

        //for testing

        private static void itemKiiras(Item a)
        {
            if (a.isList)
            {
                Console.Write("[");
                foreach (var item in a.list)
                {
                    itemKiiras(item);
                }
                Console.Write("]");
            }
            else
            {
                Console.Write(a.value + ",");
            }
        }

        private static void kiirasKilencedik(List<(int x, int y)> headsAndTails)
        {
            for (int l = 18; l >= -18; l--)
            {
                for (int j = -18; j <= 18; j++)
                {
                    bool megvan = false;
                    for (int zz = 0; zz < headsAndTails.Count; zz++)
                    {
                        if (headsAndTails[zz] != (j, l))
                            continue;

                        Console.Write(zz + " ");
                        megvan = true;
                        break;
                    }
                    if (!megvan)
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------");
        }

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
            int n = 0;
            while (!int.TryParse(Console.ReadLine(), out n))
                Console.WriteLine("Az nem egy szám!");
            
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
                case 9:
                    Kilencedik();
                    break;
                case 10:
                    Tizedik();
                    break;
                case 11:
                    Tizenegyedik();
                    break;
                case 12:
                    Tizenkettedik();
                    break;
                case 13:
                    Tizenharmadik();
                    break;
                case 14:
                    Tizennegyedik();
                    break;
                default:
                    Console.WriteLine("Nincs ilyen feladat");
                    break;
            }

            Console.WriteLine("Eltelt idő: " + stopwatch.Elapsed);

        }
    }
}
