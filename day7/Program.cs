using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace day7
{
    class Program
    {
        static ColoredBagRepository repo = new ColoredBagRepository();

        static void Main(string[] args)
        {
            // Read and parse input
            var lines = System.IO.File.ReadAllLines("input.txt");
            Regex regex = new Regex(@"(?<count>\d) (?<color>.*)");
            foreach (var line in lines)
            {
                var lineParts = line.Split("bag").SkipLast(1);
                var color = lineParts.First().Trim().ToUpperInvariant();
                lineParts = lineParts.Skip(1);
                var contains = lineParts.Select(part =>
                {
                    var match = regex.Match(part);
                    if (!match.Success)
                    {
                        return (null, 0);
                    }
                    var count = int.Parse(match.Groups["count"].Value);
                    string color = match.Groups["color"].Value.Trim().ToUpperInvariant();
                    return (color: color, count: count);
                }).Where(x => x.color != null);
                var bag = new ColoredBag(color, contains.ToArray());
                repo.Store(bag);
                Debug.WriteLine(bag);
            }
            // Should have all bags loaded.
            string target = "SHINY GOLD";
            int numberOfOuterBags = 0;
            foreach (var color in repo.GetAllBagKeys())
            {
                if (BagContainsTargetBag(repo.GetBag(color), target))
                {
                    numberOfOuterBags++;
                }
            }
            Console.WriteLine(numberOfOuterBags);
            Console.WriteLine(NumberOfContainedBags(repo.GetBag(target)));
        }

        static int NumberOfContainedBags(ColoredBag bag)
        {
            if (bag == null)
            {
                throw new ArgumentNullException();
            }
            return bag.ContainableBags.Select(cb => cb.count * (1 + NumberOfContainedBags(repo.GetBag(cb.color)))).Aggregate(0, (a, b) => a + b);
        }

        static bool BagContainsTargetBag(ColoredBag bag, string target)
        {
            if (bag == null || String.IsNullOrWhiteSpace(target))
            {
                return false;
            }
            return bag.ContainableBags.Where(cb => cb.count > 0).Select(cb => cb.color).Contains(target)
                || bag.ContainableBags.Select(cb => BagContainsTargetBag(repo.GetBag(cb.color), target)).Aggregate(false, (a, b) => a || b);
        }
    }
}
