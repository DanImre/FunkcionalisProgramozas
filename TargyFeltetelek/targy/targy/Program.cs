using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace targy
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> nevek = new Dictionary<string, string>();
            Dictionary<string, List<string>> x = new Dictionary<string, List<string>>();
            using (StreamReader f = new StreamReader("bemenet.txt"))
            {
                while (!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    string[] s = sor.Split(' ');
                    if (!x.ContainsKey(s[0]))
                        x.Add(s[0], new List<string>());

                    if (!nevek.ContainsKey(s[0]))
                    {
                        string temp = "";
                        for (int i = 1; s[i].Length != 1; i++)
                            temp += s[i] + " ";

                        nevek.Add(s[0], temp);
                    }

                    if (sor.Split("IP").Count() == 2)
                        continue;

                    int index = s[0].Length-1;
                    while(index < sor.Length - 1)
                    {
                        ++index;
                        if (sor[index] != 'I' || sor[index + 1] != 'P')
                            continue;

                        int kezdoindex = index;
                        while (sor[index] != ' ' && sor[index] != ',')
                            ++index;

                        if (sor[index] == ' ' && sor[index + 1] == '(')
                        {
                            ++index;
                            while (sor[index] != ' ')
                                ++index;
                        }
                        string temp = sor.Substring(kezdoindex, index - kezdoindex);
                        if (temp.Contains('-'))
                            x[s[0]].Add(temp);
                    }

                }
            }

            List<ConsoleColor> colorList = new List<ConsoleColor>() {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta };

            Console.WriteLine("Tárgyak: '(kod) nev' ");
            int jj = 0;

            while (true)
            {
                List<string> miketirtunkki = new List<string>();
                foreach (var item in nevek)
                {
                    Console.ForegroundColor = colorList[jj % colorList.Count];
                    Console.WriteLine("(" + item.Key + ") " + item.Value + " [" + jj + "]");
                    miketirtunkki.Add(item.Key);
                    ++jj;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Parancsok:");
                Console.WriteLine("'kell [szam]' => megadja milyen targyak kellenek az adott targyhoz");
                Console.WriteLine("'mihez [szam]' => megadja milyen targyakhoz kell az adott targy");

                string[] input = Console.ReadLine().Split(' ');
                if (input.Length == 2)
                {
                    if (input[0] == "kell")
                    {
                        int lekertIndex = int.Parse(input[1]);
                        if (lekertIndex > 0 && lekertIndex < miketirtunkki.Count)
                        {
                            //List<(string nev, int melyseg)> solution = new List<(string nev, int melyseg)>();
                            List<string> solution = new List<string>();
                            List<string> stack = new List<string>();
                            if(x[miketirtunkki[lekertIndex]].Count == 0)
                                Console.WriteLine("Semmi !");
                            else
                            {
                                int melysegIndex = 0;
                                foreach (var item in x[miketirtunkki[lekertIndex]])
                                {
                                    //solution.Add((item,melysegIndex));
                                    solution.Add(item);
                                    stack.Add(item.Split(' ')[0]);

                                    while (stack.Count != 0)
                                    {
                                        //++melysegIndex;
                                        if (x.ContainsKey(stack[0]))
                                            foreach (var elem in x[stack[0]])
                                            {
                                                solution.Add(elem.Split(' ')[0]);
                                                stack.Add(elem.Split(' ')[0]);
                                            }
                                        stack.RemoveAt(0);
                                    }
                                }
                            }

                            if (solution.Count != 0)
                            {
                                Console.WriteLine("Ezek a tárgyak kellenek:");
                                for (int i = 0; i < solution.Count; i++)
                                {
                                    Console.ForegroundColor = colorList[i % colorList.Count];
                                    Console.WriteLine("(" + solution[i] + ") " + nevek[solution[i].Split(' ')[0]]);
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("roszs input, az index nem megfelelő");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else if (input[0] == "mihez")
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("roszs input, nem ertelmezett parancs");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("roszs input, Keves parameter");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
                Console.WriteLine("Ujratolteshez nyomjon barmilyen gombot!");
                jj = 0;
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
