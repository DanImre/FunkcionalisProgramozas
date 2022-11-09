using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); //nem is használom
            int e = int.Parse(Console.ReadLine());
            List<List<int>> szetvagottEgy = new List<List<int>>();
            for (int i = 0; i < e; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                List<int> tempLista = new List<int>();
                for (int j = 1; j < s.Length; j++)
                    tempLista.Add(int.Parse(s[j]));

                szetvagottEgy.Add(tempLista);
            }
            int m = int.Parse(Console.ReadLine());
            List<List<int>> szetvagottketto = new List<List<int>>();
            for (int i = 0; i < m; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                List<int> tempLista = new List<int>();
                for (int j = 1; j < s.Length; j++)
                    tempLista.Add(int.Parse(s[j]));

                szetvagottketto.Add(tempLista);
            }

            //1. lassú
            /*
            List<List<int>> permutation = DoPermute(szetvagottEgy, 0, szetvagottEgy.Count-1, new List<List<int>>());

            List<int> solution = PermutationUntillFound(szetvagottketto, 0, szetvagottketto.Count - 1, permutation);
            foreach (var item in solution)
                Console.Write(item + " ");*/

            //2.
            List<int> solution = new List<int>();
            int whichOne = 1;
            foreach (var szalagEgy in szetvagottEgy)
                foreach (var szalagKetto in szetvagottketto)
                    if (szalagEgy[0] == szalagKetto[0])
                    {
                        if (szalagEgy.Count > szalagKetto.Count)
                            solution = new List<int>(szalagEgy);
                        else
                        {
                            solution = new List<int>(szalagKetto);
                            whichOne = 2;
                        }

                        break;
                    }

            bool vege = false;
            while (!vege)
            {
                if (whichOne == 1)
                {
                    foreach (var item in szetvagottketto)
                    {
                        if (!item.Contains(solution[solution.Count - 1]))
                            continue;

                        if (item[item.Count - 1] == solution[solution.Count - 1])
                        {
                            vege = true;
                            break;
                        }

                        whichOne = 2;
                        bool mehetbele = false;
                        foreach (var i in item)
                        {
                            if (mehetbele)
                                solution.Add(i);
                            if (i == solution[solution.Count - 1])
                                mehetbele = true;
                        }

                        break;
                    }

                    continue; //else helyett
                }

                foreach (var item in szetvagottEgy)
                {
                    if (item.Contains(solution[solution.Count - 1]))
                        continue;

                    if (item[item.Count - 1] == solution[solution.Count - 1])
                    {
                        vege = true;
                        break;
                    }

                    whichOne = 1;
                    bool mehetbele = false;
                    foreach (var i in item)
                    {
                        if (mehetbele)
                            solution.Add(i);
                        if (i == solution[solution.Count - 1])
                            mehetbele = true;
                    }

                    break;
                }
            }

            foreach (var item in solution)
                Console.Write(item + " ");
        }

        static List<int> PermutationUntillFound(List<List<int>> nums, int start, int end, List<List<int>> list)
        {
            List<List<int>> solution = new List<List<int>>();
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                List<int> templist = combine(nums);

                bool theSame = false;
                foreach (var item in list)
                {
                    bool foundsame = true;
                    for (int i = 0; i < item.Count; i++)
                    {
                        if(item[i] != templist[i])
                        {
                            foundsame = false;
                            break;
                        }
                    }
                    if(foundsame)
                    {
                        theSame = true;
                        break;
                    }
                }

                if (theSame)
                {
                    return templist;
                }
                else
                    return new List<int>();
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    //swap
                    var temp = nums[start];
                    nums[start] = nums[i];
                    nums[i] = temp;

                    solution.Add(PermutationUntillFound(nums, start + 1, end, list));

                    //swap back
                    nums[i] = nums[start];
                    nums[start] = temp;
                }
            }

            foreach (var item in solution)
            {
                if (item.Count > 0)
                    return item;
            }

            return new List<int>();
        }

        static List<List<int>> DoPermute(List<List<int>> nums, int start, int end, List<List<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(combine(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    //swap
                    var temp = nums[start];
                    nums[start] = nums[i];
                    nums[i] = temp;

                    DoPermute(nums, start + 1, end, list);

                    //swap back
                    nums[i] = nums[start];
                    nums[start] = temp;
                }
            }

            return list;
        }

        static List<int> combine(List<List<int>> nums)
        {
            List<int> result = new List<int>();
            foreach (var item in nums)
                foreach (var i in item)
                    result.Add(i);

            return result;
        }
    }
}
