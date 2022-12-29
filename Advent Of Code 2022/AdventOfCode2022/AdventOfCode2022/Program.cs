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
            string[] s = File.ReadAllLines("tizennegyedik.txt");

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


        class Sensor
        {
            public int Distance { get; set; }
            public (int x, int y) BeaconPos { get; set; }
            public (int x, int y) SensorPos { get; set; }
            public Sensor((int x, int y) beacon, (int x, int y) sensor)
            {
                this.Distance = Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
                this.BeaconPos = beacon;
                this.SensorPos = sensor;
            }

            public (int startX, int endX) HowManyAtYCoordinate(int y)
            {
                //there are Distance*2 + 1 at SensorPos.y
                //after that it decreases by 2 every time

                int distanceFromSensorOnYCoords = Math.Abs(SensorPos.y - y);

                //if it's outside
                if (distanceFromSensorOnYCoords > Distance)
                    return (0, -1); //wont run the 'for' on it

                //leftside first coord (startY) and on the right (endY)
                int left = SensorPos.x - (Distance - distanceFromSensorOnYCoords);
                int right = SensorPos.x + (Distance - distanceFromSensorOnYCoords);

                //if it's inside
                return (left, right);
            }
        }

        public static void Tizenotodik()
        {
            string[] s = File.ReadAllLines("tizenotodik.txt");

            List<Sensor> x = new List<Sensor>();

            HashSet<(int x, int y)> uniqueBeacons = new HashSet<(int x, int y)>();

            foreach (var item in s)
            {
                //from : Sensor at x=2, y=18: closest beacon is at x=-2, y=15
                //to : x=2, y=18 and x=-2, y=15
                string[] temp = item.Split(':').Select(kk => kk.Substring(kk.IndexOf("at") + 3)).ToArray();
                int[] tempSensor = temp[0].Split(',').Select(kk => int.Parse(kk.Substring(kk.IndexOf('=') + 1))).ToArray();
                int[] tempBeacon = temp[1].Split(',').Select(kk => int.Parse(kk.Substring(kk.IndexOf('=') + 1))).ToArray();

                x.Add(new Sensor((tempBeacon[0], tempBeacon[1]), (tempSensor[0], tempSensor[1])));

                //doesn't add it if its already in
                uniqueBeacons.Add((tempBeacon[0], tempBeacon[1]));
            }

            int sum = 0;
            //used x map
            HashSet<int> hash = new HashSet<int>();
            foreach (var item in x)
            {
                (int startX, int endX) = item.HowManyAtYCoordinate(2000000);

                for (int i = startX; i <= endX; i++)
                {
                    //if we already counted there
                    if (hash.Contains(i))
                        continue;

                    ++sum;
                    hash.Add(i);
                }
            }
            Console.WriteLine("Positions that cannot contain a beacon at y=2000000 :" + (sum - uniqueBeacons.Count(kk => kk.y == 2000000)));

            //2. rész

            int alsoHatar = 0;
            int felsoHatar = 4000000;

            for (int i = 0; i < x.Count; i++)
            {
                bool meglett = false;
                //körbejárjuk
                (int x, int y) middle = x[i].SensorPos;
                int distance = x[i].Distance + 1; //outside the diamond
                //-x -> '0' -ig
                //downLeft
                int aktY = middle.y;
                int mettol = middle.x - distance;
                int meddig = middle.x;
                for (int j = mettol; j < meddig; j++)
                {
                    bool itsCovered = false;
                    for (int l = 0; l < x.Count; l++)
                    {
                        if (i == l)
                            continue;

                        if (Math.Abs(j - x[l].SensorPos.x) + Math.Abs(aktY - x[l].SensorPos.y) > x[l].Distance)
                            continue;

                        itsCovered = true;
                        break;
                    }

                    if(!itsCovered && j >= alsoHatar && j <= felsoHatar && aktY >= alsoHatar && aktY <= felsoHatar)
                    {
                        Console.Write("Solution! " + j  + ", " + aktY + " | ");
                        Console.WriteLine("It's tuning frequency is: " + ((j * (long)4000000) + aktY));
                        meglett = true;
                        break;
                    }

                    ++aktY;
                }

                if (meglett)
                    break;

                //downRight
                aktY = middle.y + distance;
                mettol = middle.x;
                meddig = middle.x + distance;
                for (int j = mettol; j < meddig; j++)
                {
                    bool itsCovered = false;
                    for (int l = 0; l < x.Count; l++)
                    {
                        if (i == l)
                            continue;

                        if (Math.Abs(j - x[l].SensorPos.x) + Math.Abs(aktY - x[l].SensorPos.y) > x[l].Distance)
                            continue;

                        itsCovered = true;
                        break;
                    }

                    if (!itsCovered && j >= alsoHatar && j <= felsoHatar && aktY >= alsoHatar && aktY <= felsoHatar)
                    {
                        Console.Write("Solution! " + j + ", " + aktY + " | ");
                        Console.WriteLine("It's tuning frequency is: " + ((j * (long)4000000) + aktY));
                        meglett = true;
                        break;
                    }

                    --aktY;
                }

                if (meglett)
                    break;

                //upRight
                aktY = middle.y;
                mettol = middle.x + distance;
                meddig = middle.x;
                for (int j = mettol; j > meddig; j--)
                {
                    bool itsCovered = false;
                    for (int l = 0; l < x.Count; l++)
                    {
                        if (i == l)
                            continue;

                        if (Math.Abs(j - x[l].SensorPos.x) + Math.Abs(aktY - x[l].SensorPos.y) > x[l].Distance)
                            continue;

                        itsCovered = true;
                        break;
                    }

                    if (!itsCovered && j >= alsoHatar && j <= felsoHatar && aktY >= alsoHatar && aktY <= felsoHatar)
                    {
                        Console.Write("Solution! " + j + ", " + aktY + " | ");
                        Console.WriteLine("It's tuning frequency is: " + ((j * (long)4000000) + aktY));
                        meglett = true;
                        break;
                    }

                    --aktY;
                }

                if (meglett)
                    break;

                //upLeft
                aktY = middle.y - distance;
                mettol = middle.x;
                meddig = middle.x - distance;
                for (int j = mettol; j > meddig; j--)
                {
                    bool itsCovered = false;
                    for (int l = 0; l < x.Count; l++)
                    {
                        if (i == l)
                            continue;

                        if (Math.Abs(j - x[l].SensorPos.x) + Math.Abs(aktY - x[l].SensorPos.y) > x[l].Distance)
                            continue;

                        itsCovered = true;
                        break;
                    }

                    if (!itsCovered && j >= alsoHatar && j <= felsoHatar && aktY >= alsoHatar && aktY <= felsoHatar)
                    {
                        Console.Write("Solution! " + j + ", " + aktY + " | ");
                        Console.WriteLine("It's tuning frequency is: " + ((j * (long)4000000) + aktY));
                        meglett = true;
                        break;
                    }

                    ++aktY;
                }

                if (meglett)
                    break;

            }

        }

        public static void Tizenhatodik()  
        {
            string[] s = File.ReadAllLines("tizenhatodikarpi.txt");
            Dictionary<string, (List<string> connections, int flow)> x = new Dictionary<string, (List<string> connections, int flow)>();
            foreach (var item in s)
            {
                string name = item.Substring(6, 2);
                int flow = int.Parse(item.Split(';').First().Substring(item.Split(';').First().IndexOf('=') + 1));

                List<string> connections = new List<string>();
                string[] temp = item.Split(' ');
                for (int i = 9; i < temp.Length; i++)
                    if(temp[i].Contains(','))
                        connections.Add(temp[i].Remove(2).Trim());
                    else
                        connections.Add(temp[i].Trim());


                x.Add(name, (connections, flow));
            }

            //gráfosabb
            Dictionary<string, int> distBetweenEveryNode = new Dictionary<string, int>();
            //AA -> BB (5 lépés) => "AABB", 5
            foreach (var from in x)
            {
                //setup
                Dictionary<string, int> dist = new Dictionary<string, int>();
                foreach (var item in x)
                    dist.Add(item.Key, int.MaxValue);

                dist[from.Key] = 0;

                Queue<string> q = new Queue<string>();
                q.Enqueue(from.Key);

                //actual algorithm
                while (q.Count != 0)
                {
                    var temp = q.Dequeue();

                    int nextDist = dist[temp] + 1;

                    foreach (var item in x[temp].connections)
                    {
                        if (dist[item] <= nextDist)
                            continue;

                        q.Enqueue(item);
                        dist[item] = nextDist;
                    }
                }

                foreach (var item in dist)
                {
                    if (item.Key == from.Key || x[item.Key].flow == 0)
                        continue;

                    distBetweenEveryNode.Add(from.Key + item.Key, item.Value);
                }
            }

            Dictionary<string, (List<string> connections, int flow)> nodes = new Dictionary<string, (List<string> connections, int flow)>();
            foreach (var item in x)
                if (item.Value.flow != 0)
                    nodes.Add(item.Key, item.Value);

            Console.WriteLine("The most pressure I can release: " + TizenhatodikOkosabbRek("AA",distBetweenEveryNode,new HashSet<string>(),0,0,nodes,30));

            //2. rész

            //Main idea:
            //First I go, then the elephant, but it cant collect the same things me

            Console.WriteLine("With you and an elephant working together for 26 minutes: " + TizenhatodikMasodikReszOkosabbRek("AA", distBetweenEveryNode, new HashSet<string>(), 0, 0, nodes, 26));
            
            //Túl lassú
            //Console.WriteLine("With you and an elephant working together for 26 minutes: " + TizenhatodikMasodikReszRek("AA", "AA", distBetweenEveryNode, new HashSet<string>(), 0, 0, 0, nodes, 26,26));
        }
        /*
        //for a DP solution:
        public static Dictionary<long, int> tizenhatodikDP = new Dictionary<long, int>();*/
        public static int TizenhatodikOkosabbRek(string aktElem, Dictionary<string,int> dist, HashSet<string> nyitott, int flowPerMinute, int flowSum, Dictionary<string, (List<string> connections, int flow)> nodes, int time)
        {
            int max = 0;
            foreach (var item in nodes)
            {
                if (item.Key == aktElem || nyitott.Contains(item.Key) || time - dist[aktElem + item.Key] - 1 < 0)
                    continue;

                nyitott.Add(item.Key);
                int temp = TizenhatodikOkosabbRek(item.Key, dist, nyitott, flowPerMinute + item.Value.flow, flowSum + flowPerMinute * (dist[aktElem + item.Key] + 1), nodes, time - (dist[aktElem + item.Key] + 1));
                nyitott.Remove(item.Key);
                if (temp > max)
                    max = temp;
            }

            if(max == 0)
                return flowSum + (flowPerMinute * time);

            return max;
        }

        //nem használjuk
        public static int TizenhatodikMasodikReszRek(string aktElem, string elefantAktElem, Dictionary<string, int> dist, HashSet<string> nyitott, int flowPerMinute, int flowPerMinuteElefant, int flowSum, Dictionary<string, (List<string> connections, int flow)> nodes, int time, int timeElefant)
        {
            List<int> maxValues = new List<int>() { 0 };
            foreach (var item in nodes)
            {
                if (nyitott.Contains(item.Key))
                    continue;

                nyitott.Add(item.Key);

                if (item.Key != aktElem && time - dist[aktElem + item.Key] - 1 >= 0)
                    maxValues.Add(TizenhatodikMasodikReszRek(item.Key, elefantAktElem, dist, nyitott, flowPerMinute + item.Value.flow, flowPerMinuteElefant, flowSum + flowPerMinute * (dist[aktElem + item.Key] + 1), nodes, time - (dist[aktElem + item.Key] + 1), timeElefant));
                
                if (item.Key != elefantAktElem && timeElefant - dist[elefantAktElem + item.Key] - 1 >= 0)
                    maxValues.Add(TizenhatodikMasodikReszRek(aktElem, item.Key, dist, nyitott, flowPerMinute, flowPerMinuteElefant + item.Value.flow, flowSum + flowPerMinuteElefant * (dist[elefantAktElem + item.Key] + 1), nodes, time ,timeElefant - (dist[elefantAktElem + item.Key] + 1)));
                
                nyitott.Remove(item.Key);
            }

            if (maxValues.Count == 1)
                maxValues.Add(flowSum + (flowPerMinute * time) + (flowPerMinuteElefant * timeElefant));

            return maxValues.Max();
        }
        
        public static int TizenhatodikMasodikReszOkosabbRek(string aktElem, Dictionary<string, int> dist, HashSet<string> nyitott, int flowPerMinute, int flowSum, Dictionary<string, (List<string> connections, int flow)> nodes, int time)
        {
            int max = 0;
            foreach (var item in nodes)
            {
                if (nyitott.Contains(item.Key) || item.Key == aktElem || time - dist[aktElem + item.Key] - 1 < 0)
                    continue;

                nyitott.Add(item.Key);
                //én megyek
                int temp = TizenhatodikMasodikReszOkosabbRek(item.Key, dist, nyitott, flowPerMinute + item.Value.flow, flowSum + flowPerMinute * (dist[aktElem + item.Key] + 1), nodes, time - (dist[aktElem + item.Key] + 1));

                //elefánt része
                int temp2 = TizenhatodikOkosabbRek("AA", dist, nyitott, 0, 0, nodes, 26);
                //én eddigi cuccom:
                temp2 += flowSum + (flowPerMinute * (dist[aktElem + item.Key] + 1)) //maradék idő az úton át
                    + ((flowPerMinute + item.Value.flow) * (time - dist[aktElem + item.Key] - 1)); //miután odaértem a végéig

                nyitott.Remove(item.Key);

                if (temp2 > temp)
                    temp = temp2;

                if (temp > max)
                    max = temp;
            }

            int temp3 = flowSum +(flowPerMinute * time);

            if (temp3 > max)
                return temp3;

            return max;
        }

        class Ko
        {
            public int szelesseg { get; set; }
            public int magassag { get; set; }
            public (int x, int y) position { get; set; }
            public HashSet<(int x, int y)> body { get; set; }

            public Ko((int x, int y) pos, HashSet<(int x, int y)> body)
            {
                this.position = pos;
                this.body = body;
                szelesseg = 1 + body.Max(kk => kk.x);
                magassag = 1 - body.Min(kk => kk.y);
            }

            public Ko(Ko a)
            {
                this.position = a.position;
                this.body = new HashSet<(int x, int y)>(a.body);
                szelesseg = 1 + body.Max(kk => kk.x);
                magassag = 1 - body.Min(kk => kk.y);
            }

            public bool isColliding(HashSet<(int x, int y)> a)
            {
                return body.Any(kk => a.Contains((kk.x + position.x, kk.y + position.y)));
            }

            public IEnumerable<(int x, int y)> actualPositions ()
            {
                return body.Select(kk => (kk.x + position.x, kk.y + position.y));
            }
        }

        class State
        {
            public int whichRock { get; set; }
            public int heightWhenAdded { get; set; }
            public int wind { get; set; }

            //last 100 rows:
            //top contains the topmost positions per x coordinate, relative to the smallest one
            public HashSet<int> top { get; set; } //HashSet better for this, by design see: .SetEquals()

            public State(int rockIndex, int height, int windIndex, List<int> top)
            {
                whichRock = rockIndex;
                heightWhenAdded = height;
                wind = windIndex;
                this.top = new HashSet<int>(top);
            }

            public static bool operator ==(State a, State b)
            {
                return a.whichRock == b.whichRock
                    && a.wind == b.wind
                    && a.top.SetEquals(b.top);
            }
            public static bool operator !=(State a, State b)
            {
                return !(a == b);
            }

            //overriding default equals check
            public override bool Equals(object obj)
            {
                if (obj == null || !this.GetType().Equals(obj.GetType()))
                    return false;

                State a = (State)obj;
                return this == a;
            }
            //overriding it's HashCode
            //Probably won't need it
            public override int GetHashCode()
            {
                return HashCode.Combine(whichRock, wind, top);
            }
        }

        public static void Tizenhetedik()
        {
            int[] s = File.ReadAllLines("tizenhetedik.txt").First().Select(kk => kk - 61).ToArray();

            HashSet<(int x, int y)> cords = new HashSet<(int x, int y)>();
            for (int i = 0; i < 7; i++)
                cords.Add((i, 0));

            //szimulálós
            List<Ko> otKo = new List<Ko>();
            otKo.Add(new Ko((0, 0), new HashSet<(int x, int y)>() { (0, 0), (1, 0), (2, 0), (3, 0) })); //####
            otKo.Add(new Ko((0, 0), new HashSet<(int x, int y)>() { (1, 0), (0, -1), (1, -1), (2, -1), (1,-2) })); //+
            otKo.Add(new Ko((0, 0), new HashSet<(int x, int y)>() { (2, 0), (2, -1), (0, -2), (1, -2), (2,-2) })); //fordított L
            otKo.Add(new Ko((0, 0), new HashSet<(int x, int y)>() { (0, 0), (0, -1), (0, -2), (0, -3)})); //I
            otKo.Add(new Ko((0, 0), new HashSet<(int x, int y)>() { (0, 0), (1, 0), (0, -1), (1, -1)})); //negyzet

            int legmagasabbY = 0;

            long kovek = 0;
            int szel = 0;

            while (kovek < 2022)
            {
                //Console.Write($"\r{Math.Round(kovek/2022.0 * 100)}%   " );

                Ko aktKo = new Ko(otKo[(int)(kovek % 5)]);

                aktKo.position = (2, legmagasabbY + 4 + Math.Abs(aktKo.body.Min(kk => kk.y)));

                while(!aktKo.isColliding(cords))
                {
                    int xhova = aktKo.position.x + s[szel % s.Length];
                    if (xhova >= 0 && xhova + aktKo.szelesseg - 1 <= 6)
                    {
                        aktKo.position = (xhova, aktKo.position.y);
                        if (aktKo.isColliding(cords))
                        {
                            aktKo.position = (aktKo.position.x - s[szel % s.Length], aktKo.position.y - 1);
                            ++szel;
                            continue;
                        }
                    }
                    aktKo.position = (aktKo.position.x, aktKo.position.y - 1);

                    ++szel;
                }

                aktKo.position = (aktKo.position.x, aktKo.position.y + 1);

                IEnumerable<(int x, int y)> realWolrdCords = aktKo.actualPositions();
                int maxY = realWolrdCords.Max(kk => kk.y);

                if (maxY > legmagasabbY)
                    legmagasabbY = maxY;

                foreach (var item in realWolrdCords)
                    cords.Add(item);

                ++kovek;
            }


            //Console.WriteLine();
            Console.WriteLine(cords.Max(kk => kk.y));

            //2. rész

            cords = new HashSet<(int x, int y)>();
            for (int i = 0; i < 7; i++)
                cords.Add((i, 0));

            legmagasabbY = 0;

            kovek = 0;
            szel = 0;

            //new things:
            List<int> top = new List<int>();
            for (int i = 0; i < 7; i++)
                top.Add(0);

            List<State> states = new List<State>();

            while (kovek < 1000000000000)
            {
                //Console.Write($"\r{Math.Round(kovek/2022.0 * 100)}%   " );

                Ko aktKo = new Ko(otKo[(int)(kovek % 5)]);

                aktKo.position = (2, legmagasabbY + 4 + Math.Abs(aktKo.body.Min(kk => kk.y)));

                State newstate = new State((int)(kovek % 5), legmagasabbY, szel % s.Length, top);

                foreach (var item in states)
                {
                    if (item != newstate)
                        continue;

                    Console.WriteLine("Megvan!!!!");
                    break;
                }

                while (!aktKo.isColliding(cords))
                {
                    int xhova = aktKo.position.x + s[szel % s.Length];
                    if (xhova >= 0 && xhova + aktKo.szelesseg - 1 <= 6)
                    {
                        aktKo.position = (xhova, aktKo.position.y);
                        if (aktKo.isColliding(cords))
                        {
                            aktKo.position = (aktKo.position.x - s[szel % s.Length], aktKo.position.y - 1);
                            ++szel;
                            continue;
                        }
                    }
                    aktKo.position = (aktKo.position.x, aktKo.position.y - 1);

                    ++szel;
                }

                aktKo.position = (aktKo.position.x, aktKo.position.y + 1);

                IEnumerable<(int x, int y)> realWolrdCords = aktKo.actualPositions();
                int maxY = realWolrdCords.Max(kk => kk.y);

                //top update:
                int minBody = aktKo.body.Min(kk => kk.y);
                foreach (var item in aktKo.body)
                    if (top[item.x + aktKo.position.x] < item.y + minBody)
                        top[item.x + aktKo.position.x] = item.y + minBody;

                //then make 'top' relative:


                if (maxY > legmagasabbY)
                    legmagasabbY = maxY;

                foreach (var item in realWolrdCords)
                    cords.Add(item);

                ++kovek;
            }

        }

        class Side
        {
            public (int x, int y, int z) position { get; set; }
            public char paralelAxis { get; set; }
            public Side((int x, int y, int z) Cord, char axis)
            {
                position = Cord;
                paralelAxis = axis;
            }

            public static bool operator ==(Side a, Side b)
            {
                return a.position == b.position
                    && a.paralelAxis == b.paralelAxis;
            }
            
            public static bool operator !=(Side a, Side b)
            {
                return !(a == b);
            }

            //overriding default equals check
            public override bool Equals(object obj)
            {
                if (obj == null || !this.GetType().Equals(obj.GetType()))
                    return false;

                Side a = (Side)obj;
                return this == a;
            }
            //overriding it's HashCode
            //Probably won't need it
            public override int GetHashCode()
            {
                return HashCode.Combine(position, paralelAxis);
            }
        }

        public static void Tizennyolcadik()
        {
            string[] s = File.ReadAllLines("tizennyolcadikproba.txt");
            //s = new string[] { "1,1,1", "2,1,1" };
            HashSet<Side> sides = new HashSet<Side>();
            foreach (var item in s)
            {
                string[] temp = item.Split(',');
                int x = int.Parse(temp[0]);
                int y = int.Parse(temp[1]);
                int z = int.Parse(temp[2]);

                //there are 6 sides:
                //x:
                Side actSide = new Side((x, y, z), 'x');
                if (!sides.Remove(actSide))
                    sides.Add(actSide);
                //x-2:
                actSide = new Side((x, y, z - 1), 'x');
                if (!sides.Remove(actSide))
                    sides.Add(actSide);
                //y:
                actSide = new Side((x + 1, y, z), 'y');
                if (!sides.Remove(actSide))
                    sides.Add(actSide);
                //y-2:
                actSide = new Side((x, y, z), 'y');
                if (!sides.Remove(actSide))
                    sides.Add(actSide);
                //z:
                actSide = new Side((x, y, z), 'z');
                if (!sides.Remove(actSide))
                    sides.Add(actSide);
                //z-2:
                actSide = new Side((x, y + 1, z), 'z');
                if (!sides.Remove(actSide))
                    sides.Add(actSide);
            }
            Console.WriteLine("The surface area of my scanned lava droplet: " + sides.Count);

            //2. feladat
            /*
            int eredmeny = sides.Count; //1. feladatból

            int legkisebbX = sides.Min(kk => kk.position.x) - 10;
            int legnagyobbX = sides.Max(kk => kk.position.x) + 10;
            int legkisebbY = sides.Min(kk => kk.position.y) - 10;
            int legnagyobbY = sides.Max(kk => kk.position.y) + 10;
            int legkisebbZ = sides.Min(kk => kk.position.z) - 10;
            int legnagyobbZ = sides.Max(kk => kk.position.z) + 10;
            */

            //valahogy végig kell menni a shape' oldalain, ahogy connectelnek, lehet gráfosan

            //1. elem a külsején kéne legyen
            int legkisebbZ = sides.Min(kk => kk.position.z);
            Side elso = sides.Where(kk => kk.paralelAxis == 'x' && kk.position.z == legkisebbZ).First();

            HashSet<Side> kulsok = new HashSet<Side>();
            Queue<Side> q = new Queue<Side>();
            q.Enqueue(elso);

            while (q.Count != 0)
            {
                Side actSide = q.Dequeue();
                kulsok.Add(actSide);

                //neighbor check
                //12 lesz összesen

                //felfelé hajlók

                //hosszabbítások
                Side x1 = new Side((actSide.position.x + 1, actSide.position.y, actSide.position.z), actSide.paralelAxis);
                Side x2 = new Side((actSide.position.x, actSide.position.y + 1, actSide.position.z), actSide.paralelAxis);
                Side x3 = new Side((actSide.position.x - 1, actSide.position.y, actSide.position.z), actSide.paralelAxis);
                Side x4 = new Side((actSide.position.x, actSide.position.y - 1, actSide.position.z), actSide.paralelAxis);

            }
        }

        public class Blueprint
        {
            public int maxOreCost { get; set; }
            public int maxClayCost { get; set; }
            public int maxObsidianCost { get; set; }

            public int oreCount = 0;
            public int clayCount = 0;
            public int obsidianCount = 0;
            public int geodeCount = 0;

            public int orePerMinute = 1;
            public int clayPerMinute = 0;
            public int obsidianPerMinute = 0;
            public int geodePerMinute = 0;

            public int blueprintnumber { get; set; }
            public int oreRobotCost { get; set; }
            public int clayRobotCost { get; set; }
            public (int ore, int clay) obsidianRobotCost { get; set; }
            public (int ore, int obsidian) geodeRobotCost { get; set; }

            public Blueprint(int blueprint, int oreRobot, int clayRobot, (int ore, int clay) obsidianRobot, (int ore, int clay) geodeRobot)
            {
                blueprintnumber = blueprint;
                oreRobotCost = oreRobot;
                clayRobotCost = clayRobot;
                obsidianRobotCost = obsidianRobot;
                geodeRobotCost = geodeRobot;

                maxOreCost = Math.Max(Math.Max(oreRobotCost, clayRobotCost), Math.Max(obsidianRobotCost.ore, geodeRobotCost.ore));
                maxClayCost = obsidianRobotCost.clay;
                maxObsidianCost = geodeRobotCost.obsidian;
            }

            public void Update()
            {
                oreCount += orePerMinute;
                clayCount += clayPerMinute;
                obsidianCount += obsidianPerMinute;
                geodeCount += geodePerMinute;
            }
            public void ReverseUpdate()
            {
                oreCount -= orePerMinute;
                clayCount -= clayPerMinute;
                obsidianCount -= obsidianPerMinute;
                geodeCount -= geodePerMinute;
            }
        }

        public static void TizenKilencedik()
        {
            string[] s = File.ReadAllLines("tizenkilencedik.txt");

            List<Blueprint> x = new List<Blueprint>();

            foreach (var item in s)
            {
                string[] temp = item.Split('.');
                int oreRobotCost = int.Parse(temp[0].Split(' ')[6]);
                int clayRobotCost = int.Parse(temp[1].Trim().Split(' ')[4]);
                int obsidianRobotOreCost = int.Parse(temp[2].Trim().Split(' ')[4]);
                int obsidianRobotClayCost = int.Parse(temp[2].Trim().Split(' ')[7]);
                int geodeRobotOreCost = int.Parse(temp[3].Trim().Split(' ')[4]);
                int geodeRobotObsidianCost = int.Parse(temp[3].Trim().Split(' ')[7]);

                x.Add(new Blueprint(x.Count + 1, oreRobotCost, clayRobotCost, (obsidianRobotOreCost, obsidianRobotClayCost), (geodeRobotOreCost, geodeRobotObsidianCost)));
            }

            long total = 0;
            foreach (var item in x)
                total += item.blueprintnumber * TizenkilencedikJobbRek(item, new Dictionary<(int, int, int, int, int, int, int, int, int), int>(), 24);

            Console.WriteLine("What do you get if you add up the quality level of all of the blueprints in your list? " + total);

            //2. rész
            total = 1;
            foreach (var item in x.Take(3))
                total *= TizenkilencedikJobbRek(item, new Dictionary<(int, int, int, int, int, int, int, int, int), int>(), 32);

            Console.WriteLine("What do you get if you multiply the maximum geodes per blueprint together? " + total);
        }

        public static int TizenkilencedikJobbRek(Blueprint bp, Dictionary<(int, int, int, int, int, int, int, int, int), int> cache, int time)
        {
            if (time == 0)
                return bp.geodeCount;

            var key = (time, bp.orePerMinute, bp.clayPerMinute, bp.obsidianPerMinute, bp.geodePerMinute, bp.oreCount, bp.clayCount, bp.obsidianCount, bp.geodeCount);
            if (cache.ContainsKey(key))
                return cache[key];

            //Semmit tevés
            int max = bp.geodeCount + bp.geodePerMinute * time;

            int oldOreCount = bp.oreCount;
            int oldClayCount = bp.clayCount;
            int oldObsidianCount = bp.obsidianCount;
            int oldGeodeCount = bp.geodeCount;

            //oreRobot
            if (bp.maxOreCost > bp.orePerMinute) //még nincs annyi, amennyit el tudunk kolteni
            {
                int timeToWaitUntilItsBuilt = (int)Math.Ceiling((bp.oreRobotCost - bp.oreCount) / (double)bp.orePerMinute);

                if (timeToWaitUntilItsBuilt < 0)
                    timeToWaitUntilItsBuilt = 0;

                int timeRemaining = time - timeToWaitUntilItsBuilt - 1;
                if (timeRemaining > 0)
                {
                    //Nem tárolunk többet, mint amennyit fel tudnánk használni (cache miatt)
                    bp.oreCount = Math.Min(bp.oreCount + bp.orePerMinute * (timeToWaitUntilItsBuilt + 1) - bp.oreRobotCost, timeRemaining * bp.maxOreCost);
                    bp.clayCount = Math.Min(bp.clayCount + bp.clayPerMinute * (timeToWaitUntilItsBuilt + 1), timeRemaining * bp.maxClayCost);
                    bp.obsidianCount = Math.Min(bp.obsidianCount + bp.obsidianPerMinute * (timeToWaitUntilItsBuilt + 1), timeRemaining * bp.maxObsidianCost);
                    bp.geodeCount += bp.geodePerMinute * (timeToWaitUntilItsBuilt + 1);
                    ++bp.orePerMinute;

                    int temp = TizenkilencedikJobbRek(bp, cache, timeRemaining);
                    if (temp > max)
                        max = temp;

                    --bp.orePerMinute;
                    bp.oreCount = oldOreCount;
                    bp.clayCount = oldClayCount;
                    bp.obsidianCount = oldObsidianCount;
                    bp.geodeCount = oldGeodeCount;
                }
            }
            //clayRobot
            if (bp.maxClayCost > bp.clayPerMinute) //még nincs annyi, amennyit el tudunk kolteni
            {
                int timeToWaitUntilItsBuilt = (int)Math.Ceiling((bp.clayRobotCost - bp.oreCount) / (double)bp.orePerMinute);

                if (timeToWaitUntilItsBuilt < 0)
                    timeToWaitUntilItsBuilt = 0;

                int timeRemaining = time - timeToWaitUntilItsBuilt - 1;
                if (timeRemaining > 0)
                {
                    //Nem tárolunk többet, mint amennyit fel tudnánk használni (cache miatt)
                    bp.oreCount = Math.Min(bp.oreCount + bp.orePerMinute * (timeToWaitUntilItsBuilt + 1) - bp.clayRobotCost, timeRemaining * bp.maxOreCost);
                    bp.clayCount = Math.Min(bp.clayCount + bp.clayPerMinute * (timeToWaitUntilItsBuilt + 1), timeRemaining * bp.maxClayCost);
                    bp.obsidianCount = Math.Min(bp.obsidianCount + bp.obsidianPerMinute * (timeToWaitUntilItsBuilt + 1), timeRemaining * bp.maxObsidianCost);
                    bp.geodeCount += bp.geodePerMinute * (timeToWaitUntilItsBuilt + 1);
                    ++bp.clayPerMinute;

                    int temp = TizenkilencedikJobbRek(bp, cache, timeRemaining);
                    if (temp > max)
                        max = temp;
                    
                    --bp.clayPerMinute;
                    bp.oreCount = oldOreCount;
                    bp.clayCount = oldClayCount;
                    bp.obsidianCount = oldObsidianCount;
                    bp.geodeCount = oldGeodeCount;
                }
            }
            //obsidianRobot
            if (bp.clayPerMinute != 0 && bp.maxObsidianCost > bp.obsidianPerMinute) //még nincs annyi, amennyit el tudunk kolteni
            {
                int timeToWaitUntilItsBuilt = Math.Max((int)Math.Ceiling((bp.obsidianRobotCost.clay - bp.clayCount) / (double)bp.clayPerMinute), (int)Math.Ceiling((bp.obsidianRobotCost.ore - bp.oreCount) / (double)bp.orePerMinute));

                if (timeToWaitUntilItsBuilt < 0)
                    timeToWaitUntilItsBuilt = 0;

                int timeRemaining = time - timeToWaitUntilItsBuilt - 1;
                if (timeRemaining > 0)
                {
                    //Nem tárolunk többet, mint amennyit fel tudnánk használni (cache miatt)
                    bp.oreCount = Math.Min(bp.oreCount + bp.orePerMinute * (timeToWaitUntilItsBuilt + 1) - bp.obsidianRobotCost.ore, timeRemaining * bp.maxOreCost);
                    bp.clayCount = Math.Min(bp.clayCount + bp.clayPerMinute * (timeToWaitUntilItsBuilt + 1) - bp.obsidianRobotCost.clay, timeRemaining * bp.maxClayCost);
                    bp.obsidianCount = Math.Min(bp.obsidianCount + bp.obsidianPerMinute * (timeToWaitUntilItsBuilt + 1), timeRemaining * bp.maxObsidianCost);
                    bp.geodeCount += bp.geodePerMinute * (timeToWaitUntilItsBuilt + 1);
                    ++bp.obsidianPerMinute;

                    int temp = TizenkilencedikJobbRek(bp, cache, timeRemaining);
                    if (temp > max)
                        max = temp;
                    
                    --bp.obsidianPerMinute;
                    bp.oreCount = oldOreCount;
                    bp.clayCount = oldClayCount;
                    bp.obsidianCount = oldObsidianCount;
                    bp.geodeCount = oldGeodeCount;
                }
            }

            //geodeRobot
            if (bp.obsidianPerMinute != 0)
            {
                int timeToWaitUntilItsBuiltGeode = Math.Max((int)Math.Ceiling((bp.geodeRobotCost.obsidian - bp.obsidianCount) / (double)bp.obsidianPerMinute), (int)Math.Ceiling((bp.geodeRobotCost.ore - bp.oreCount) / (double)bp.orePerMinute));

                if (timeToWaitUntilItsBuiltGeode < 0)
                    timeToWaitUntilItsBuiltGeode = 0;

                int timeRemainingGeode = time - timeToWaitUntilItsBuiltGeode - 1;
                if (timeRemainingGeode > 0)
                {
                    //Nem tárolunk többet, mint amennyit fel tudnánk használni (cache miatt)
                    bp.oreCount = Math.Min(bp.oreCount + bp.orePerMinute * (timeToWaitUntilItsBuiltGeode + 1) - bp.geodeRobotCost.ore, timeRemainingGeode * bp.maxOreCost);
                    bp.clayCount = Math.Min(bp.clayCount + bp.clayPerMinute * (timeToWaitUntilItsBuiltGeode + 1), timeRemainingGeode * bp.maxClayCost);
                    bp.obsidianCount = Math.Min(bp.obsidianCount + bp.obsidianPerMinute * (timeToWaitUntilItsBuiltGeode + 1) - bp.geodeRobotCost.obsidian, timeRemainingGeode * bp.maxObsidianCost);
                    bp.geodeCount += bp.geodePerMinute * (timeToWaitUntilItsBuiltGeode + 1);
                    ++bp.geodePerMinute;

                    int temp = TizenkilencedikJobbRek(bp, cache, timeRemainingGeode);
                    
                    if (temp > max)
                        max = temp;

                    --bp.geodePerMinute;
                    bp.oreCount = oldOreCount;
                    bp.clayCount = oldClayCount;
                    bp.obsidianCount = oldObsidianCount;
                    bp.geodeCount = oldGeodeCount;
                }
            }

            cache.Add(key, max);
            return max;
        }

        class Node
        {
            public object Content { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            public Node(object content)
            {
                this.Content = content;
            }
        }

        class LinkedList<T>
        {
            public int Count()
            {
                return count;
            }

            private int count = 0;

            public Node Fejelem { get; set; }
            public LinkedList()
            {
                this.Fejelem = new Node(null);
                this.Fejelem.Next = Fejelem;
                this.Fejelem.Previous = Fejelem;
                count = 0;
            }

            public void Add(T content)
            {
                Node node = new Node(content);

                ++count;
                if (count == 1)
                {
                    Fejelem = node;
                    Fejelem.Next = Fejelem;
                    Fejelem.Previous = Fejelem;
                    return;
                }

                Node aktelem = Fejelem.Previous;

                aktelem.Next = node;
                node.Previous = aktelem;
                node.Next = Fejelem;
                Fejelem.Previous = node;
            }

            public bool Add(Node node)
            {
                if (!typeof(T).Equals(node.Content.GetType()))
                    return false;

                ++count;
                if (count == 1)
                {
                    Fejelem = node;
                    Fejelem.Next = Fejelem;
                    return true;
                }

                Node aktelem = Fejelem;
                while (aktelem.Next != Fejelem)
                    aktelem = aktelem.Next;

                aktelem.Next = node;
                node.Previous = aktelem;
                aktelem.Next.Next = Fejelem;
                Fejelem.Previous = node;

                return true;
            }

            public void Print()
            {
                Node aktelem = Fejelem.Next;
                Console.Write(Fejelem.Content + " ");
                while (aktelem != Fejelem)
                {
                    Console.Write(aktelem.Content + " ");
                    aktelem = aktelem.Next;
                }
                Console.WriteLine();
            }

            public int IndexOf(Node node)
            {
                if (node == Fejelem)
                    return 0;

                int counter = 1;
                Node aktelem = Fejelem.Next;
                while (aktelem != node && aktelem != Fejelem)
                {
                    aktelem = aktelem.Next;
                    ++counter;
                }
                if (counter == count)
                    return -1;

                return counter;
            }

            public bool Contains(Node node)
            {
                return this.IndexOf(node) != -1;
            }
            //új :
            public void Move(Node node)
            {
                Node aktelem = Fejelem;
                while (aktelem.Next != node)
                    aktelem = aktelem.Next;

                if (Fejelem == node)
                    Fejelem = node.Next;

                // AKT - NODE - TEMP => AKT - TEMP - NODE

                Node temp = aktelem.Next.Next;

                aktelem.Next = temp;

                temp.Previous = aktelem;
                node.Previous = temp;
                node.Next = temp.Next; //X - X => X - O - X
                temp.Next.Previous = node;
                temp.Next = node;

            }
            public void MoveBack(Node node)
            {
                Node aktelem = node.Previous;

                if (Fejelem == node)
                    Fejelem = node.Next;

                Node temp = aktelem.Previous;

                node.Next.Previous = aktelem;
                aktelem.Next = node.Next;

                aktelem.Previous = node;
                node.Next = aktelem;
                temp.Next = node;
                node.Previous = temp;
                /*
                if (node.Next == Fejelem)
                    Fejelem = node;*/
            }
            public bool PlaceAfter(Node node, int where)
            {
                /*
                if (!this.Contains(node))
                    return false;*/

                Node aktelem = Fejelem;
                while (aktelem.Next != node)
                    aktelem = aktelem.Next;

                Node whereNode = GetNodeAt(where);

                if (Fejelem == node)
                    Fejelem = node.Next;

                aktelem.Next = node.Next;// X - O - X => X - X
 
                node.Next = whereNode.Next; //X - X => X - O - X
                whereNode.Next = node;

                return true;
            }

            #region comment
            /*
            //regi :
            public bool Move(Node node)
            {
                if (!this.Contains(node))
                    return false;

                Node aktelem = Fejelem;
                while (aktelem.Next != node)
                    aktelem = aktelem.Next;

                Node temp = node.Next.Next;

                aktelem.Next = node.Next;
                aktelem.Next.Next = node;
                aktelem.Next.Next.Next = temp;

                if (Fejelem == node)
                    Fejelem = aktelem.Next;

                return true;
            }

            public bool MoveBack(Node node)
            {
                if (!this.Contains(node))
                    return false;

                Node aktelem = Fejelem;
                while (aktelem.Next.Next != node)
                    aktelem = aktelem.Next;

                Node temp = aktelem.Next;
                Node temp2 = node.Next;

                aktelem.Next = node;
                aktelem.Next.Next = temp;
                aktelem.Next.Next.Next = temp2;

                return true;
            }*/
            #endregion

            public Node GetNodeAt(int index)
            {
                Node aktelem = Fejelem;
                while (index > 0)
                {
                    aktelem = aktelem.Next;
                    --index;
                }

                return aktelem;
            }

            public bool Remove(Node node)
            {
                if (!Contains(node))
                    return false;

                Node aktelem = Fejelem;
                while (aktelem.Next != node)
                    aktelem = aktelem.Next;

                aktelem.Next = node.Next;

                return true;
            }
        }

        public static void Huszadik()
        {
            LinkedList<int> linked = new LinkedList<int>();

            List<Node> nodes = new List<Node>();

            string[] s = File.ReadAllLines("huszadik.txt");
            foreach (var item in s)
            {
                Node temp = new Node(int.Parse(item));
                nodes.Add(temp);
                linked.Add(temp);
            }

            double counter = 1;
            /*
            Console.WriteLine();
            linked.Print();
            Console.WriteLine();*/
            foreach (var item in nodes)
            {
                Console.Write("\r{0}%  ", Math.Round(counter / (nodes.Count - 1) * 100, 2));

                if ((int)item.Content == 0)
                    continue;
                int howMuch = 0;
                if ((int)item.Content / (double)nodes.Count > 0)
                {
                    if ((int)item.Content < 0)
                        howMuch = (int)item.Content + nodes.Count;
                    else
                        howMuch = (int)item.Content + nodes.Count;
                }

                howMuch %= nodes.Count - 1;

                for (int i = 0; i < howMuch; i++)
                {
                    linked.Move(item);
                }
                for (int i = 0; i > howMuch; i--)
                {
                    linked.MoveBack(item);
                }

                ++counter;
            }

            int zeroNodeIndex = linked.IndexOf(nodes.Where(kk => (int)kk.Content == 0).First());
            int firstNumber = (int)linked.GetNodeAt(zeroNodeIndex + 1000).Content;
            int secondNumber = (int)linked.GetNodeAt(zeroNodeIndex + 2000).Content;
            int thirdNumber = (int)linked.GetNodeAt(zeroNodeIndex + 3000).Content;

            Console.WriteLine();
            Console.WriteLine(firstNumber + " " + secondNumber + " " + thirdNumber);
            Console.WriteLine("The sum of the three numbers that form the grove coordinates: " + (firstNumber + secondNumber + thirdNumber));
            
            //14 perc 45 sec volt

            Console.WriteLine();
        }

        class YellingMonkeys
        {
            public static Dictionary<string, YellingMonkeys> Monkeys { get; set; }

            private bool numberYeller { get; set; }

            public long Number { get; set; }

            public YellingMonkeys LeftSide { get; set; }
            public YellingMonkeys RightSide { get; set; }

            public Func<YellingMonkeys, YellingMonkeys, long> Operation;

            public YellingMonkeys(int number)
            {
                this.numberYeller = true;
                this.Number = number;
            }
            public YellingMonkeys()
            {
                this.numberYeller = false;
            }

            public bool isYeller() => numberYeller;

            public long GetValue()
            {
                if (numberYeller)
                    return Number;

                return Operation(LeftSide, RightSide);
            }

            public static long operator +(YellingMonkeys a, YellingMonkeys b)
            {
                return a.GetValue() + b.GetValue();
            }

            public static long operator -(YellingMonkeys a, YellingMonkeys b)
            {
                return a.GetValue() - b.GetValue();
            }

            public static long operator /(YellingMonkeys a, YellingMonkeys b)
            {
                return a.GetValue() / b.GetValue();
            }
            public static long operator *(YellingMonkeys a, YellingMonkeys b)
            {
                return a.GetValue() * b.GetValue();
            }
        }

        public static void Huszonegyedik()
        {
            string[] s = File.ReadAllLines("huszonegyedik.txt");

            YellingMonkeys.Monkeys = new Dictionary<string, YellingMonkeys>();

            //need all the monkeys first
            foreach (var item in s)
            {
                string[] temp = item.Split(':').Select(kk => kk.Trim()).ToArray();
                
                int number = -1;
                if (int.TryParse(temp[1], out number))
                    YellingMonkeys.Monkeys.Add(temp[0],new YellingMonkeys(number));
                else
                    YellingMonkeys.Monkeys.Add(temp[0], new YellingMonkeys());
            }

            //getting the operations
            foreach (var item in s)
            {
                string[] temp = item.Split(':').Select(kk => kk.Trim()).ToArray();

                if (YellingMonkeys.Monkeys[temp[0]].isYeller())
                    continue;
                
                if (temp[1].Contains('+'))
                {
                    string[] theTwoMonkey = temp[1].Split('+').Select(kk => kk.Trim()).ToArray();
                    YellingMonkeys.Monkeys[temp[0]].Operation = (kk, zz) => kk + zz;
                    YellingMonkeys.Monkeys[temp[0]].LeftSide = YellingMonkeys.Monkeys[theTwoMonkey[0]];
                    YellingMonkeys.Monkeys[temp[0]].RightSide = YellingMonkeys.Monkeys[theTwoMonkey[1]];
                }
                else if (temp[1].Contains('-'))
                {
                    string[] theTwoMonkey = temp[1].Split('-').Select(kk => kk.Trim()).ToArray();
                    YellingMonkeys.Monkeys[temp[0]].Operation = (kk, zz) => kk - zz;
                    YellingMonkeys.Monkeys[temp[0]].LeftSide = YellingMonkeys.Monkeys[theTwoMonkey[0]];
                    YellingMonkeys.Monkeys[temp[0]].RightSide = YellingMonkeys.Monkeys[theTwoMonkey[1]];
                }
                else if (temp[1].Contains('/'))
                {
                    string[] theTwoMonkey = temp[1].Split('/').Select(kk => kk.Trim()).ToArray();
                    YellingMonkeys.Monkeys[temp[0]].Operation = (kk, zz) => kk / zz;
                    YellingMonkeys.Monkeys[temp[0]].LeftSide = YellingMonkeys.Monkeys[theTwoMonkey[0]];
                    YellingMonkeys.Monkeys[temp[0]].RightSide = YellingMonkeys.Monkeys[theTwoMonkey[1]];
                }
                else if (temp[1].Contains('*'))
                {
                    string[] theTwoMonkey = temp[1].Split('*').Select(kk => kk.Trim()).ToArray();
                    YellingMonkeys.Monkeys[temp[0]].Operation = (kk, zz) => kk * zz;
                    YellingMonkeys.Monkeys[temp[0]].LeftSide = YellingMonkeys.Monkeys[theTwoMonkey[0]];
                    YellingMonkeys.Monkeys[temp[0]].RightSide = YellingMonkeys.Monkeys[theTwoMonkey[1]];
                }
            }

            Console.WriteLine("The root monkey will yell: " + YellingMonkeys.Monkeys["root"].GetValue());

            //2. rész

            Console.WriteLine("The number I need to yell is: ");
        }

        public static void Huszonkettedik()
        {
            string[] s = File.ReadAllLines("huszonkettedik.txt");
            //List<List<char>> map = File.ReadAllLines("huszonkettedikproba.txt").Select(kk => kk.ToList()).ToList();
            List<List<char>> map = new List<List<char>>();
            foreach (var item in s)
            {
                if (item == "")
                    break;

                map.Add(item.ToList());
            }
            //-1 => 'R' | -2 => 'L'
            List<int> movesList = new List<int>();

            string moves = s.Last();
            while (true)
            {
                int index = 0;
                while (index != moves.Length && moves[index] != 'R' && moves[index] != 'L')
                    ++index;

                movesList.Add(int.Parse(moves.Substring(0, index)));

                if (index == moves.Length)
                    break;

                movesList.Add(moves[index] == 'R' ? -1 : -2);
                moves = moves.Substring(index + 1);
            }


            (int x, int y) pos = (0, 0);
            while (map[pos.y][pos.x] != '.')
            {
                if (map[pos.y].Count - 1 == pos.x)
                    pos = (0, pos.y + 1);
                else
                    pos = (pos.x + 1, pos.y);
            }

            (int x, int y) howToMove = (1, 0);

            foreach (var item in movesList)
            {
                //R
                if (item == -1)
                {
                    if (howToMove == (1, 0))
                        howToMove = (0, 1);
                    else if (howToMove == (0, 1))
                        howToMove = (-1, 0);
                    else if (howToMove == (-1, 0))
                        howToMove = (0, -1);
                    else if (howToMove == (0, -1))
                        howToMove = (1, 0);
                }
                //L
                else if (item == -2)
                {
                    if (howToMove == (1, 0))
                        howToMove = (0, -1);
                    else if (howToMove == (0, 1))
                        howToMove = (1, 0);
                    else if (howToMove == (-1, 0))
                        howToMove = (0, 1);
                    else if (howToMove == (0, -1))
                        howToMove = (-1, 0);
                }
                //just move
                else
                    for (int i = 0; i < item; i++)
                    {
                        (int x, int y) nextPos = (pos.x + howToMove.x, pos.y + howToMove.y);

                        //Vertical wrapping
                        if (howToMove.y != 0)
                        {
                            if (nextPos.y < 0)
                            {
                                nextPos = (nextPos.x, map.Count - 1);
                                while (nextPos.y >= 0 && (nextPos.x >= map[nextPos.y].Count || map[nextPos.y][nextPos.x] == ' '))
                                    nextPos = (nextPos.x, nextPos.y - 1);

                                if (nextPos.y == -1)
                                    break;
                            }
                            else if (nextPos.y >= map.Count)
                            {
                                nextPos = (nextPos.x, 0);
                                while (nextPos.y < map.Count && (nextPos.x >= map[nextPos.y].Count || map[nextPos.y][nextPos.x] == ' '))
                                    nextPos = (nextPos.x, nextPos.y + 1);

                                if (nextPos.y == map.Count)
                                    break;
                            }
                            else if (howToMove.y == 1 && (nextPos.x >= map[nextPos.y].Count || map[nextPos.y][nextPos.x] == ' '))
                            {
                                nextPos = (nextPos.x, 0);
                                while (nextPos.y < map.Count && (nextPos.x >= map[nextPos.y].Count || map[nextPos.y][nextPos.x] == ' '))
                                    nextPos = (nextPos.x, nextPos.y + 1);

                                if (nextPos.y == -1)
                                    break;
                            }
                            else if (howToMove.y == -1 && map[nextPos.y][nextPos.x] == ' ')
                            {
                                nextPos = (nextPos.x, map.Count - 1);
                                while (nextPos.y >= 0 && (nextPos.x >= map[nextPos.y].Count || map[nextPos.y][nextPos.x] == ' '))
                                    nextPos = (nextPos.x, nextPos.y - 1);

                                if (nextPos.y == -1)
                                    break;
                            }
                        }
                        else
                        {
                            //Horizontal wrapping
                            if (nextPos.x < 0)
                                nextPos = (map[nextPos.y].Count - 1, nextPos.y);
                            else if (nextPos.x >= map[nextPos.y].Count)
                            {
                                nextPos = (0, nextPos.y);
                                while (nextPos.x < map[nextPos.y].Count && map[nextPos.y][nextPos.x] == ' ')
                                    nextPos = (nextPos.x + 1, nextPos.y);

                                if (nextPos.x == map[nextPos.y].Count)
                                    break;
                            }
                            else if (howToMove.x == 1 && map[nextPos.y][nextPos.x] == ' ')
                            {
                                nextPos = (0, nextPos.y);
                                while (nextPos.x < map[nextPos.y].Count && map[nextPos.y][nextPos.x] == ' ')
                                    nextPos = (nextPos.x + 1, nextPos.y);

                                if (nextPos.x == map[nextPos.y].Count)
                                    break;
                            }
                            else if (howToMove.x == -1 && map[nextPos.y][nextPos.x] == ' ')
                                nextPos = (map[nextPos.y].Count - 1, nextPos.y);
                        }

                        if (map[nextPos.y][nextPos.x] != '.')
                            break;

                        pos = nextPos;
                        /*
                        //kiiras
                        for (int j = 0; j < map.Count; j++)
                        {
                            for (int k = 0; k < map[j].Count; k++)
                            {
                                if((k,j) == pos)
                                    Console.Write("$");
                                else
                                    Console.Write(map[j][k]);
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("--------------------");
                        Console.WriteLine();*/
                    }
            }

            long facingValue = 0;
            if (howToMove.y == -1)
                facingValue = 1;
            else if (howToMove.x == -1)
                facingValue = 2;
            else if (howToMove.y == 1)
                facingValue = 3;

            Console.WriteLine("The final password is: " + (1000 * (long)(pos.y + 1) + 4 * (long)(pos.x + 1) + facingValue));
        }

        class Elf
        {
            public (int x, int y) Position { get; set; }
            public Elf((int x, int y) pos)
            {
                this.Position = pos;
            }

            //Handling them as tuples
            public override bool Equals(object obj)
            {
                if (obj == null || !this.GetType().Equals(obj.GetType()))
                    return false;

                Elf a = (Elf)obj;
                return this.Position == a.Position;
            }

            public override int GetHashCode()
            {
                return Position.GetHashCode();
            }
        }

        public static void Huszonharmadik()
        {
            string[] s = File.ReadAllLines("huszonharmadik.txt");

            HashSet<Elf> positions = new HashSet<Elf>();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '#')
                        positions.Add(new Elf((j, i)));

            Queue<int> directions = new Queue<int>();
            directions.Enqueue(0);//North
            directions.Enqueue(1);//South
            directions.Enqueue(2);//West
            directions.Enqueue(3);//East

            int rounds = 0;
            while (rounds < 10)
            {
                //Console.Write("\r{0} ",rounds);
                bool hadToMove = false;
                
                Dictionary<(int x, int y), Elf> dic = new Dictionary<(int x, int y), Elf>();
                foreach (var item in positions)
                {
                    bool jobbra = false;
                    bool jobbrafel = false;
                    bool fel = false;
                    bool balrafel = false;
                    bool balra = false;
                    bool balrale = false;
                    bool le = false;
                    bool jobbrale = false;

                    foreach (var i in positions)
                    {
                        //Jobbra
                        if (i.Position == (item.Position.x + 1, item.Position.y))
                            jobbra = true;
                        //Balra
                        else if (i.Position == (item.Position.x - 1, item.Position.y))
                            balra = true;
                        //Fel
                        else if (i.Position == (item.Position.x, item.Position.y - 1))
                            fel = true;
                        //Le
                        else if (i.Position == (item.Position.x, item.Position.y + 1))
                            le = true;
                        //Jobbrafel
                        else if (i.Position == (item.Position.x + 1, item.Position.y - 1))
                            jobbrafel = true;
                        //Balrafel
                        else if (i.Position == (item.Position.x - 1, item.Position.y - 1))
                            balrafel = true;
                        //Balrale
                        else if (i.Position == (item.Position.x - 1, item.Position.y + 1))
                            balrale = true;
                        //Jobbrale
                        else if (i.Position == (item.Position.x + 1, item.Position.y + 1))
                            jobbrale = true;
                    }

                    if (!jobbra
                        && !jobbrafel
                        && !fel
                        && !balrafel
                        && !balra
                        && !balrale
                        && !le
                        && !jobbrale)
                    {
                        continue;
                    }
                    hadToMove = true;

                    //only 2 Elfs would move to the same position
                    bool canMove = false;
                    foreach (var dir in directions)
                    {
                        //North
                        if (dir == 0 && !balrafel && !fel && !jobbrafel)
                        {
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x, item.Position.y - 1)))
                                dic.Add((item.Position.x, item.Position.y - 1), item);
                            else
                                dic.Remove((item.Position.x, item.Position.y - 1));

                            break;
                        }
                        //South
                        else if (dir == 1 && !balrale && !le && !jobbrale)
                        {
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x, item.Position.y + 1)))
                                dic.Add((item.Position.x, item.Position.y + 1), item);
                            else
                                dic.Remove((item.Position.x, item.Position.y + 1));

                            break;
                        }
                        //West
                        else if (dir == 2 && !balrale && !balra && !balrafel)
                        {
                            if (rounds == 1 && item.Position.x == 3 && item.Position.y == 1)
                            {
                                Console.WriteLine("MoveLeft");
                                Console.WriteLine(balrale + " " + balra + " " + balrafel);
                                Console.WriteLine(positions.Contains(new Elf((item.Position.x - 1, item.Position.y - 1))));

                                foreach (var ii in positions)
                                {
                                    if (ii.Position == (2, 0))
                                    {
                                        Console.WriteLine("BROOOOOOOO");
                                        break;
                                    }
                                }
                                canMove = true;
                                if (!dic.ContainsKey((item.Position.x - 1, item.Position.y)))
                                    dic.Add((item.Position.x - 1, item.Position.y), item);
                                else
                                    dic.Remove((item.Position.x - 1, item.Position.y));

                                break;
                            }
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x - 1, item.Position.y)))
                                dic.Add((item.Position.x - 1, item.Position.y), item);
                            else
                                dic.Remove((item.Position.x - 1, item.Position.y));

                            break;
                        }
                        //East
                        else if (dir == 3 && !jobbrale && !jobbra && !jobbrafel)
                        {
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x + 1, item.Position.y)))
                                dic.Add((item.Position.x + 1, item.Position.y), item);
                            else
                                dic.Remove((item.Position.x + 1, item.Position.y));

                            break;
                        }
                    }
                    if(!canMove)
                        dic.Add((item.Position.x, item.Position.y), item);
                }

                ++rounds;

                if (!hadToMove)
                    break;

                //Cycling the directions
                directions.Enqueue(directions.Dequeue());

                //Moving the pieces
                foreach (var item in dic)
                    item.Value.Position = item.Key;
            }

            //4254
            Console.WriteLine("How many empty ground tiles does that rectangle contain? " + ((positions.Max(kk => kk.Position.x) - positions.Min(kk => kk.Position.x) + 1) * (positions.Max(kk => kk.Position.y) - positions.Min(kk => kk.Position.y) + 1) - positions.Count));

            //2. rész

            while (true)
            {
                //Console.Write("\r{0} ",rounds);
                bool hadToMove = false;

                Dictionary<(int x, int y), Elf> dic = new Dictionary<(int x, int y), Elf>();
                foreach (var item in positions)
                {
                    bool jobbra = false;
                    bool jobbrafel = false;
                    bool fel = false;
                    bool balrafel = false;
                    bool balra = false;
                    bool balrale = false;
                    bool le = false;
                    bool jobbrale = false;

                    foreach (var i in positions)
                    {
                        //Jobbra
                        if (i.Position == (item.Position.x + 1, item.Position.y))
                            jobbra = true;
                        //Balra
                        else if (i.Position == (item.Position.x - 1, item.Position.y))
                            balra = true;
                        //Fel
                        else if (i.Position == (item.Position.x, item.Position.y - 1))
                            fel = true;
                        //Le
                        else if (i.Position == (item.Position.x, item.Position.y + 1))
                            le = true;
                        //Jobbrafel
                        else if (i.Position == (item.Position.x + 1, item.Position.y - 1))
                            jobbrafel = true;
                        //Balrafel
                        else if (i.Position == (item.Position.x - 1, item.Position.y - 1))
                            balrafel = true;
                        //Balrale
                        else if (i.Position == (item.Position.x - 1, item.Position.y + 1))
                            balrale = true;
                        //Jobbrale
                        else if (i.Position == (item.Position.x + 1, item.Position.y + 1))
                            jobbrale = true;
                    }

                    if (!jobbra
                        && !jobbrafel
                        && !fel
                        && !balrafel
                        && !balra
                        && !balrale
                        && !le
                        && !jobbrale)
                    {
                        continue;
                    }
                    hadToMove = true;

                    //only 2 Elfs would move to the same position
                    bool canMove = false;
                    foreach (var dir in directions)
                    {
                        //North
                        if (dir == 0 && !balrafel && !fel && !jobbrafel)
                        {
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x, item.Position.y - 1)))
                                dic.Add((item.Position.x, item.Position.y - 1), item);
                            else
                                dic.Remove((item.Position.x, item.Position.y - 1));

                            break;
                        }
                        //South
                        else if (dir == 1 && !balrale && !le && !jobbrale)
                        {
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x, item.Position.y + 1)))
                                dic.Add((item.Position.x, item.Position.y + 1), item);
                            else
                                dic.Remove((item.Position.x, item.Position.y + 1));

                            break;
                        }
                        //West
                        else if (dir == 2 && !balrale && !balra && !balrafel)
                        {
                            if (rounds == 1 && item.Position.x == 3 && item.Position.y == 1)
                            {
                                Console.WriteLine("MoveLeft");
                                Console.WriteLine(balrale + " " + balra + " " + balrafel);
                                Console.WriteLine(positions.Contains(new Elf((item.Position.x - 1, item.Position.y - 1))));

                                foreach (var ii in positions)
                                {
                                    if (ii.Position == (2, 0))
                                    {
                                        Console.WriteLine("BROOOOOOOO");
                                        break;
                                    }
                                }
                                canMove = true;
                                if (!dic.ContainsKey((item.Position.x - 1, item.Position.y)))
                                    dic.Add((item.Position.x - 1, item.Position.y), item);
                                else
                                    dic.Remove((item.Position.x - 1, item.Position.y));

                                break;
                            }
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x - 1, item.Position.y)))
                                dic.Add((item.Position.x - 1, item.Position.y), item);
                            else
                                dic.Remove((item.Position.x - 1, item.Position.y));

                            break;
                        }
                        //East
                        else if (dir == 3 && !jobbrale && !jobbra && !jobbrafel)
                        {
                            canMove = true;
                            if (!dic.ContainsKey((item.Position.x + 1, item.Position.y)))
                                dic.Add((item.Position.x + 1, item.Position.y), item);
                            else
                                dic.Remove((item.Position.x + 1, item.Position.y));

                            break;
                        }
                    }
                    if (!canMove)
                        dic.Add((item.Position.x, item.Position.y), item);
                }

                ++rounds;

                if (!hadToMove)
                    break;

                //Cycling the directions
                directions.Enqueue(directions.Dequeue());

                //Moving the pieces
                foreach (var item in dic)
                    item.Value.Position = item.Key;
            }

            Console.WriteLine(" What is the number of the first round where no Elf moves? " + rounds);
        }

        public class Wind
        {
            public static (int width, int height) BoardSize { get; set; }

            //0 right | 1 up | 2 left | 3 down
            public int Direction { get; set; }
            public (int x, int y) Pos { get; set; }

            public Wind((int x, int y) pos, int direction)
            {
                this.Pos = pos;
                this.Direction = direction;
            }

            public Wind(Wind a)
            {
                this.Pos = a.Pos;
                this.Direction = a.Direction;
            }

            public void Move()
            {
                (int x, int y) nextpos = (0,0);
                switch(Direction)
                {
                    case 0:
                        nextpos = (Pos.x + 1, Pos.y);
                        break;
                    case 1:
                        nextpos = (Pos.x, Pos.y - 1);
                        break;
                    case 2:
                        nextpos = (Pos.x - 1, Pos.y);
                        break;
                    case 3:
                        nextpos = (Pos.x, Pos.y + 1);
                        break;
                    default:
                        break;
                }

                if (nextpos.x == BoardSize.width)
                    nextpos.x = 1;
                else if (nextpos.x == 0)
                    nextpos.x = BoardSize.width - 1;
                else if (nextpos.y == BoardSize.height)
                    nextpos.y = 1;
                else if (nextpos.y == 0)
                    nextpos.y = BoardSize.height - 1;

                Pos = nextpos;
            }
        }

        public static void Huszonnegyedik()
        {
            string[] s = File.ReadAllLines("huszonnegyedik.txt");

            Wind.BoardSize = (s[0].Length - 1, s.Length - 1);
            List<Wind> winds = new List<Wind>();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == '>')
                        winds.Add(new Wind((j, i), 0));
                    else if (s[i][j] == '^')
                        winds.Add(new Wind((j, i), 1));
                    else if (s[i][j] == '<')
                        winds.Add(new Wind((j, i), 2));
                    else if (s[i][j] == 'v')
                        winds.Add(new Wind((j, i), 3));
                }

            int cycle = 0;

            for (int i = 1; i < (s.Length - 2); i++)
            {
                int mult = (s[0].Length - 2) * i;
                if (mult % (s.Length - 2) == 0)
                    cycle = mult;
            }
            if (cycle == 0)
                cycle = (s[0].Length - 2) * (s.Length - 2);

            //Mikor nem lehet odalépni
            List<List<HashSet<int>>> Tiles = new List<List<HashSet<int>>>();
            for (int i = 0; i < s.Length - 2; i++)
            {
                List<HashSet<int>> temp = new List<HashSet<int>>();
                for (int j = 0; j < s[i].Length - 2; j++)
                    temp.Add(new HashSet<int>());
                Tiles.Add(temp);
            }

            //needed for second part
            foreach (var item in winds)
                Tiles[item.Pos.y - 1][item.Pos.x - 1].Add(0);

            for (int i = 1; i <= cycle; i++)
            {
                foreach (var item in winds)
                {
                    item.Move();
                    Tiles[item.Pos.y - 1][item.Pos.x - 1].Add(i);
                }
            }

            (int x, int y) winPos = (Tiles[0].Count - 1, Tiles.Count - 1);
            int quickest = Tiles.Count * Tiles[0].Count;
            HashSet<(int x, int y, int time)> tried = new HashSet<(int x, int y, int time)>();
            for (int i = 1; i <= cycle; i++)
            {
                if (Tiles[0][0].Contains(i))
                    continue;
                
                Queue<(int x, int y, int time)> q = new Queue<(int x, int y, int time)>();
                q.Enqueue((0, 0, i));
                while (q.Count != 0)
                {
                    var akt = q.Dequeue();

                    if (akt.x == winPos.x && akt.y == winPos.y)
                    {
                        if (akt.time + 1 < quickest)
                            quickest = akt.time + 1;
                        continue;
                    }
                    
                    if (akt.time >= quickest)
                        continue;

                    //marad
                    if (!tried.Contains((akt.x, akt.y, akt.time + 1)))
                        if (!Tiles[akt.y][akt.x].Contains((akt.time + 1)%cycle))
                        {
                            q.Enqueue((akt.x, akt.y, akt.time + 1));
                            tried.Add((akt.x, akt.y, akt.time + 1));
                        }
                    //jobbra
                    if (!tried.Contains((akt.x + 1, akt.y, akt.time + 1)))
                        if (akt.x + 1 < Tiles[0].Count && !Tiles[akt.y][akt.x + 1].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x + 1, akt.y, akt.time + 1));
                            tried.Add((akt.x + 1, akt.y, akt.time + 1));
                        }
                    //fel
                    if (!tried.Contains((akt.x, akt.y - 1, akt.time + 1)))
                        if (akt.y - 1 >= 0 && !Tiles[akt.y - 1][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y - 1, akt.time + 1));
                            tried.Add((akt.x, akt.y - 1, akt.time + 1));
                        }
                    //balra
                    if (!tried.Contains((akt.x - 1, akt.y, akt.time + 1)))
                        if (akt.x - 1 >= 0 && !Tiles[akt.y][akt.x - 1].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x - 1, akt.y, akt.time + 1));
                            tried.Add((akt.x - 1, akt.y, akt.time + 1));
                        }
                    //le
                    if (!tried.Contains((akt.x, akt.y + 1, akt.time + 1)))
                        if (akt.y + 1 < Tiles.Count && !Tiles[akt.y + 1][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y + 1, akt.time + 1));
                            tried.Add((akt.x, akt.y + 1, akt.time + 1));
                        }
                }
            }
            Console.WriteLine("The fewest number of minutes required to avoid the blizzards and reach the goal: " + quickest);

            //2. rész

            //From Finish to Start
            int time = quickest;

            winPos = (0, 0);
            quickest = Tiles.Count * Tiles[0].Count + time;
            tried.Clear();

            for (int i = time; i <= cycle + time; i++)
            {
                if (Tiles[Tiles.Count - 1][Tiles[0].Count - 1].Contains(i % cycle))
                    continue;

                Queue<(int x, int y, int time)> q = new Queue<(int x, int y, int time)>();
                q.Enqueue((Tiles[0].Count - 1, Tiles.Count - 1, i));
                while (q.Count != 0)
                {
                    var akt = q.Dequeue();

                    if (akt.x == winPos.x && akt.y == winPos.y)
                    {
                        if (akt.time + 1 < quickest)
                            quickest = akt.time + 1;
                        if(akt.time + 1 == 32)
                            Console.WriteLine("kor: " + i);
                        continue;
                    }

                    if (akt.time >= quickest)
                        continue;

                    //marad
                    if (!tried.Contains((akt.x, akt.y, akt.time + 1)))
                        if (!Tiles[akt.y][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y, akt.time + 1));
                            tried.Add((akt.x, akt.y, akt.time + 1));
                        }
                    //jobbra
                    if (!tried.Contains((akt.x + 1, akt.y, akt.time + 1)))
                        if (akt.x + 1 < Tiles[0].Count && !Tiles[akt.y][akt.x + 1].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x + 1, akt.y, akt.time + 1));
                            tried.Add((akt.x + 1, akt.y, akt.time + 1));
                        }
                    //fel
                    if (!tried.Contains((akt.x, akt.y - 1, akt.time + 1)))
                        if (akt.y - 1 >= 0 && !Tiles[akt.y - 1][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y - 1, akt.time + 1));
                            tried.Add((akt.x, akt.y - 1, akt.time + 1));
                        }
                    //balra
                    if (!tried.Contains((akt.x - 1, akt.y, akt.time + 1)))
                        if (akt.x - 1 >= 0 && !Tiles[akt.y][akt.x - 1].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x - 1, akt.y, akt.time + 1));
                            tried.Add((akt.x - 1, akt.y, akt.time + 1));
                        }
                    //le
                    if (!tried.Contains((akt.x, akt.y + 1, akt.time + 1)))
                        if (akt.y + 1 < Tiles.Count && !Tiles[akt.y + 1][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y + 1, akt.time + 1));
                            tried.Add((akt.x, akt.y + 1, akt.time + 1));
                        }
                }
            }

            //From start to Finish again:

            time = quickest;

            winPos = (Tiles[0].Count - 1, Tiles.Count - 1);
            quickest = Tiles.Count * Tiles[0].Count + time;
            tried.Clear();

            for (int i = time; i <= cycle + time; i++)
            {
                if (Tiles[0][0].Contains(i % cycle))
                    continue;

                Queue<(int x, int y, int time)> q = new Queue<(int x, int y, int time)>();
                q.Enqueue((0, 0, i));
                while (q.Count != 0)
                {
                    var akt = q.Dequeue();

                    if (akt.x == winPos.x && akt.y == winPos.y)
                    {
                        if (akt.time + 1 < quickest)
                            quickest = akt.time + 1;
                        if (akt.time + 1 == 32)
                            Console.WriteLine("kor: " + i);
                        continue;
                    }

                    if (akt.time >= quickest)
                        continue;

                    //marad
                    if (!tried.Contains((akt.x, akt.y, akt.time + 1)))
                        if (!Tiles[akt.y][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y, akt.time + 1));
                            tried.Add((akt.x, akt.y, akt.time + 1));
                        }
                    //jobbra
                    if (!tried.Contains((akt.x + 1, akt.y, akt.time + 1)))
                        if (akt.x + 1 < Tiles[0].Count && !Tiles[akt.y][akt.x + 1].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x + 1, akt.y, akt.time + 1));
                            tried.Add((akt.x + 1, akt.y, akt.time + 1));
                        }
                    //fel
                    if (!tried.Contains((akt.x, akt.y - 1, akt.time + 1)))
                        if (akt.y - 1 >= 0 && !Tiles[akt.y - 1][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y - 1, akt.time + 1));
                            tried.Add((akt.x, akt.y - 1, akt.time + 1));
                        }
                    //balra
                    if (!tried.Contains((akt.x - 1, akt.y, akt.time + 1)))
                        if (akt.x - 1 >= 0 && !Tiles[akt.y][akt.x - 1].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x - 1, akt.y, akt.time + 1));
                            tried.Add((akt.x - 1, akt.y, akt.time + 1));
                        }
                    //le
                    if (!tried.Contains((akt.x, akt.y + 1, akt.time + 1)))
                        if (akt.y + 1 < Tiles.Count && !Tiles[akt.y + 1][akt.x].Contains((akt.time + 1) % cycle))
                        {
                            q.Enqueue((akt.x, akt.y + 1, akt.time + 1));
                            tried.Add((akt.x, akt.y + 1, akt.time + 1));
                        }
                }
            }

            Console.WriteLine("What is the fewest number of minutes required to reach the goal, go back to the start, then reach the goal again? " + quickest);

        }

        public static void Huszonotodik()
        {
            string[] s = File.ReadAllLines("huszonotodik.txt");

            long sum = 0;

            foreach (var item in s)
                sum += SNAFUToBaseTen(item);

            Console.WriteLine("What SNAFU number do you supply to Bob's console? " + BaseTenToSNAFU(sum));
        }

        public static long SNAFUToBaseTen(string snafu)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            dic.Add('=', -2);
            dic.Add('-', -1);
            dic.Add('0', 0);
            dic.Add('1', 1);
            dic.Add('2', 2);

            long result = 0;

            for (int i = 0; i < snafu.Length; i++)
                result += dic[snafu[i]] * (long)Math.Pow(5, snafu.Length - i - 1);

            return result;
        }

        public static string BaseTenToSNAFU(long baseTen)
        {
            string result = "";
            long activeNumber = baseTen;
            while (activeNumber != 0)
            {
                long remainder = activeNumber % 5;
                bool tookAway = false;
                if (remainder > 2)
                {
                    if (remainder == 4)
                        result += "-";
                    else
                        result += "=";

                    tookAway = true;
                }
                else
                    result += remainder;

                activeNumber = activeNumber / 5;
                if (tookAway)
                    activeNumber += 1;
            }

            string returnString = "";
            foreach (var item in result.Reverse())
                returnString += item;

            return returnString;
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
                case 15:
                    Tizenotodik();
                    break;
                case 16:
                    Tizenhatodik();
                    break;
                case 17:
                    Tizenhetedik();
                    break;
                case 18:
                    Tizennyolcadik();
                    break;
                case 19:
                    TizenKilencedik();
                    break;
                case 20:
                    Huszadik();
                    break;
                case 21:
                    Huszonegyedik();
                    break;
                case 22:
                    Huszonkettedik();
                    break;
                case 23:
                    Huszonharmadik();
                    break;
                case 24:
                    Huszonnegyedik();
                    break;
                case 25:
                    Huszonotodik();
                    break;
                default:
                    Console.WriteLine("Nincs ilyen feladat");
                    break;
            }

            Console.WriteLine("Eltelt idő: " + stopwatch.Elapsed);

        }
    }
}
