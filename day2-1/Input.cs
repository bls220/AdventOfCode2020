using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day2_1
{
    public class Input
    {
        public int MinChars { get; }
        public int MaxChars { get; }
        public Char RequiredCharacter { get; }
        public String Password { get; }

        public Input(int minChars, int maxChars, char requiredCharacter, string password)
        {
            MinChars = minChars;
            MaxChars = maxChars;
            RequiredCharacter = requiredCharacter;
            Password = password;
        }

        public override string ToString() => $"{this.MinChars}-{this.MaxChars} {this.RequiredCharacter}: {this.Password}";

        public bool IsValid()
        {
            int count = Password.Where(c => c == this.RequiredCharacter).Count();
            return count >= this.MinChars && count <= this.MaxChars;
        }

        public static Input FromString(string input)
        {
            int min, max;
            Char requiredCharacter;
            string password;
            const string pattern = @"(?<min>\d+)-(?<max>\d+) (?<letter>.): (?<password>.*)";
            Match match = Regex.Match(input, pattern);
            min = int.Parse(match.Groups["min"].Value);
            max = int.Parse(match.Groups["max"].Value);
            requiredCharacter = match.Groups["letter"].Value[0];
            password = match.Groups["password"].Value;
            return new Input(min, max, requiredCharacter, password);
        }
    }
}