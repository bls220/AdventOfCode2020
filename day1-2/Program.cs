using System;
using System.Linq;

namespace day1_2
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
                    foreach (var valueZ in values)
                    {
                        if (valueX + valueY + valueZ == 2020)
                        {
                            Console.WriteLine(valueX * valueY * valueZ);
                            return;
                        }
                    }
                }
            }
        }
    }
}
