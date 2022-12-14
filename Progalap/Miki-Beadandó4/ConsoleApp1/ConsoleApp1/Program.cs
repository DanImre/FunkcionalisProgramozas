using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the input
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int numSettlements = input[0];
            int numDays = input[1];

            // Read the temperature data
            int[][] temps = new int[numSettlements][];
            for (int i = 0; i < numSettlements; i++)
                temps[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int warmestDay = -1;

            for (int i = 0; i < numDays; i++)
            {
                List<int> seged = new List<int>();
                for (int j = 0; j < numSettlements; j++)
                    seged.Add(temps[j][i]);

                for (int z = 0; z < numDays; z++)
                {
                    if (z == i)
                        continue;

                    List<int> seged2 = new List<int>();
                    for (int j = 0; j < numSettlements; j++)
                        seged2.Add(temps[j][z]);

                    bool vegigIgaz = true;
                    for (int l = 0; l < seged.Count; l++)
                        if(seged[l] <= seged2[l])
                        {
                            vegigIgaz = false;
                            break;
                        }

                    if(vegigIgaz)
                    {
                        warmestDay = i + 1;
                        break;
                    }
                }

                if (warmestDay != -1)
                    break;
            }


            // Print the result
                Console.WriteLine("The warmest day is: " + warmestDay);
        }
    }
}
