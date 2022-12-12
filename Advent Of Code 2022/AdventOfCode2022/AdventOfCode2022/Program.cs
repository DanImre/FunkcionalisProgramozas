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

        class Item
        {
            public List<int> szorzok { get; set; }
            public Item()
            {
                szorzok = new List<int>();
            }

            public Item(int a)
            {
                szorzok = new List<int>() { 1, a };
                rendezes();
            }

            public static Item operator +(Item a, int b)
            {
                Item c = new Item();
                c.szorzok = new List<int>(a.szorzok);

                c.hozzaadas(b);

                return c;
            }

            //a == b
            public static Item operator +(Item a, Item b)
            {
                Item c = new Item();
                c.szorzok = new List<int>(a.szorzok);

                c.szorzok.Add(2);
                c.rendezes();

                return c;
            }

            public static Item operator *(Item a, int b)
            {
                Item c = new Item();
                c.szorzok = new List<int>(a.szorzok);

                c.szorzok.Add(b);

                c.rendezes();

                return c;
            }

            //a == b
            public static Item operator *(Item a, Item b)
            {
                //h a plusszok 0 - ák, akkor semmit nem kell csinálni, mert a prím tényezős szorzók ugyanazok
                Item c = new Item();
                c.szorzok = new List<int>(a.szorzok);

                foreach (var item in b.szorzok)
                    c.szorzok.Add(item);

                c.rendezes();

                return c;
            }

            public static List<int> primes;

            public void rendezes()
            {
                List<int> ujSzorzok = new List<int>();

                for (int i = 0; i < szorzok.Count; i++)
                {
                    int tempSzorzo = szorzok[i];
                    while (true)
                    {
                        bool talalt = false;
                        foreach (var item in primes)
                            if(tempSzorzo % item == 0)
                            {
                                ujSzorzok.Add(item);
                                tempSzorzo /= item;
                                talalt = true;
                            }

                        if (!talalt)
                        {
                            ujSzorzok.Add(tempSzorzo);
                            break;
                        }
                    }
                }

                //hozzaadasnal lehet szar lesz ha distinct
                //szorzok = new List<int>(ujSzorzok.Distinct());
                ujSzorzok.Remove(1);
                ujSzorzok.Add(1);
                szorzok = new List<int>(ujSzorzok);
            }

            public void hozzaadas(int a)
            {
                List<int> szorzokamikmaradnak = new List<int>() { 1 };
                int temp = a;
                while (true)
                {
                    bool talalt = false;
                    foreach (var item in szorzok)
                        if (item != 1 && temp % item == 0)
                        {
                            szorzokamikmaradnak.Add(item);
                            temp /= item;
                            talalt = true;
                        }

                    if(!talalt)
                    {
                        //nem felhasznált szorzók
                        int temptemp = 1;
                        foreach (var item in szorzok)
                            if(!szorzokamikmaradnak.Contains(item))
                            {
                                temptemp *= item;
                            }

                        //nem felhasznált szorzók + maradék
                        szorzokamikmaradnak.Add(temptemp + temp);
                        break;
                    }
                }

                //szorzok = new List<int>(szorzokamikmaradnak.Distinct());
                szorzok = new List<int>(szorzokamikmaradnak);
                rendezes();
            }

            public static int operator %(Item a, int b)
            {
                //b is prímszám

                if (a.szorzok.Contains(b))
                    return 0;
                else
                    return 1;//mindegy mit adunk viszsa csak ne legyen 0
                
            }

        }

        class MonkeyPartTwo
        {
            public int inspectedItems { get; set; }
            public List<Item> items { get; set; }
            public Func<Item, Item> operation { get; set; }
            public Func<Item, bool> test { get; set; }

            public int trueThrow { get; set; }
            public int falseThrow { get; set; }

            public MonkeyPartTwo()
            {
                inspectedItems = 0;
                items = new List<Item>();
            }

        }


        public static void Tizenegyedik()
        {
            //string[] s = File.ReadAllLines("tizenegyedikproba.txt");
            string[] s = File.ReadAllLines("tizenegyedik.txt");
            List<Monkey> x = new List<Monkey>();

            //prímek:
            Item.primes = new List<int>();
            for (int i = 2; i < 1000; i++) // 1000 ig prímek
            {
                bool oszthato = false;
                foreach (var item in Item.primes)
                    if (i % item == 0)
                    {
                        oszthato = true;
                        break;
                    }

                if (!oszthato)
                    Item.primes.Add(i);
            }

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

            x = new List<Monkey>();

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
        private static string concatStack(Stack<string> stack)
        {
            string solution = "";
            foreach (var item in stack.Where(kk => kk != "/").Select(kk => kk.Substring(1)))
                solution += @"\" + item;

            return solution;
        }

        //for testing

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
                default:
                    Console.WriteLine("Nincs ilyen feladat");
                    break;
            }

            Console.WriteLine("Eltelt idő: " + stopwatch.Elapsed);

        }
    }
}
