using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace day4
{
    public class Passport
    {
        public int BirthYear { get; private set; }
        public int IssueYear { get; private set; }
        public int ExpirationYear { get; private set; }
        public Height Height { get; private set; }
        public string HairColor { get; private set; }
        public string EyeColor { get; private set; }
        public string PassportId { get; private set; }
        public string CountryId { get; private set; }

        public static Passport Deserialize(string entry)
        {
            var passport = new Passport();
            var fields = entry.Split(" ");
            foreach (var field in fields)
            {
                var parts = field.Split(":");
                parts.Select(part => part?.Trim());
                switch (parts[0])
                {
                    case "byr":
                        {
                            if (int.TryParse(parts[1], out int year))
                            {
                                passport.BirthYear = year;
                            }
                            break;
                        }
                    case "iyr":
                        {
                            if (int.TryParse(parts[1], out int year))
                            {
                                passport.IssueYear = year;
                            }
                            break;
                        }
                    case "eyr":
                        {
                            if (int.TryParse(parts[1], out int year))
                            {
                                passport.ExpirationYear = year;
                            }
                            break;
                        }
                    case "hgt":
                        {
                            int.TryParse(new String(parts[1].TakeWhile(Char.IsDigit).ToArray()), out int value);
                            passport.Height = new Height
                            {
                                Value = value,
                                Unit = new String(parts[1].SkipWhile(Char.IsDigit).ToArray()).Trim()
                            };
                            break;
                        }
                    case "hcl":
                        {
                            passport.HairColor = parts[1].Trim();
                            break;
                        }
                    case "ecl":
                        {
                            passport.EyeColor = parts[1].Trim();
                            break;
                        }
                    case "pid":
                        {
                            passport.PassportId = parts[1].Trim();
                            break;
                        }
                    case "cid":
                        {
                            passport.CountryId = parts[1].Trim();
                            break;
                        }
                }
            }
            return passport;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool IsValid()
        {
            bool isValid = true;
            isValid &= this.BirthYear >= 1920 && this.BirthYear <= 2002;
            isValid &= this.IssueYear >= 2010 && this.IssueYear <= 2020;
            isValid &= this.ExpirationYear >= 2020 && this.ExpirationYear <= 2030;
            if (this.Height?.Unit == "cm")
            {
                isValid &= this.Height.Value >= 150 && this.Height.Value <= 193;
            }
            else if (this.Height?.Unit == "in")
            {
                isValid &= this.Height.Value >= 59 && this.Height.Value <= 76;
            }
            else
            {
                isValid = false;
            }
            isValid &= !String.IsNullOrWhiteSpace(this.HairColor) && this.HairColor.StartsWith("#") && Regex.IsMatch(new string(this.HairColor.Skip(1).ToArray()), @"[a-f0-9]{6}");
            isValid &= !String.IsNullOrWhiteSpace(this.EyeColor) && (this.EyeColor == "amb" || this.EyeColor == "blu" || this.EyeColor == "brn" || this.EyeColor == "gry" || this.EyeColor == "grn" || this.EyeColor == "hzl" || this.EyeColor == "oth");
            isValid &= !String.IsNullOrWhiteSpace(this.PassportId) && this.PassportId.All(Char.IsDigit) && this.PassportId.Length == 9;
            // Country ID is optional, don't check for it.
            return isValid;
        }
    }
}