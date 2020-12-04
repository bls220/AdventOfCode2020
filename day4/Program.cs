using System;
using System.Linq;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt");
            var passportEntries = input.Split("\n\n").Select(entry => entry.Replace("\n", " "));
            var passports = passportEntries.Select(Passport.Deserialize);

            var validPassports = passports.Where(passport => passport.IsValid()).Count();
            Console.WriteLine(validPassports);
        }
    }
}
