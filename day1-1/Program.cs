using System;
using System.Linq;

namespace day1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int[] values = lines.Select(line => Int32.Parse(line)).ToArray();
            foreach (var valueX in values)
            {
                foreach (var valueY in values)
                {
                    if (valueX + valueY == 2020)
                    {
                        Console.WriteLine(valueX * valueY);
                        return;
                    }
                }
            }
        }
    }
}
