using System;
using System.Linq;

namespace day2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var inputs = lines.Select(Input.FromString);
            int validInputs = inputs.Where(input => input.IsValid()).Count();
            Console.WriteLine($"Found {validInputs} valid inputs.");
        }
    }
}
