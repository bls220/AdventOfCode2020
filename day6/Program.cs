using System;
using System.Collections.Generic;
using System.Linq;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var groups = System.IO.File.ReadAllText("input.txt").Split("\n\n");

            // Answer 1
            //var groupAnswers = groups.Select(group => group.Distinct().Where(c => c != '\n').Count());

            var groupAnswers = groups
            .Select(group =>
                group.Split('\n')
                .Where(person => !String.IsNullOrWhiteSpace(person)))
            .Select(people =>
                people.Select(person => person.AsEnumerable())
                .Aggregate((answers1, answers2) => answers1.Intersect(answers2))
                .Count());

            var sum = groupAnswers.Aggregate((total, groupSum) => total + groupSum);

            Console.WriteLine(sum);
        }
    }
}
