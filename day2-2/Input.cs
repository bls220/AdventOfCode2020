using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day2_2
{
    public class Input
    {
        public int Position1 { get; }
        public int Position2 { get; }
        public Char RequiredCharacter { get; }
        public String Password { get; }

        public Input(int minChars, int maxChars, char requiredCharacter, string password)
        {
            Position1 = minChars;
            Position2 = maxChars;
            RequiredCharacter = requiredCharacter;
            Password = password;
        }

        public override string ToString() => $"{this.Position1}-{this.Position2} {this.RequiredCharacter}: {this.Password}";

        public bool IsValid()
        {
            return this.Password[this.Position1 - 1] == this.RequiredCharacter ^ this.Password[this.Position2 - 1] == this.RequiredCharacter;
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